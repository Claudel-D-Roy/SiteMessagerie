using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Security.Claims;
using Tp5Messagerie.Data;
using Tp5Messagerie.Entities;
using Tp5Messagerie.Models;
using Tp5Messagerie.Utilities;
using Tp5Messagerie.ViewModels.Home;

namespace Tp5Messagerie.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly DomainAsserts asserts;


        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> user, DomainAsserts asserts)
        {
            this.asserts = asserts;
            this.context = context;
            this.userManager = user;

        }

        public IActionResult Index(int page = 1, int pageSize = 5)
        {

            var messages = context.Messages
          .OrderByDescending(m => m.CreatedDate)
          .Skip((page - 1) * pageSize)
          .Take(pageSize)
          .Include(m => m.UserCom)
          .Include(m => m.Commentaires!.OrderByDescending(c => c.CreatedDate))
          .ThenInclude(c => c.IdUser)
          .ToList();

            var totalMessages = context.Messages.Count();

            var vm = new HomeVM
            {
                Messages = messages,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = totalMessages
                }
            };

            return View(vm);
        }

        public IActionResult AjouterMessage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AjouterMessage(Guid id, HomeVM vm)
        {
            var user = userManager.GetUserAsync(User).Result;
            var message = new Message
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                UserCom = user,
                CreatedDate = DateTime.Now,
                Contenu = vm.Contenu
            };
            context.Messages.Add(message);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult RepondreMessage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RepondreMessage(Guid Id, HomeVM vm)
        {
            var user = userManager.GetUserAsync(User).Result;
            var message = context.Messages.Find(Id);
            if (message == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var Commentaire = new Commentaire
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    CreatedDate = DateTime.Now,
                    IdUser = user,
                    MessageID = Id,
                    Contenu = vm.Contenu,

                };
                context.Commentaires.Add(Commentaire);

                context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult RemoveMessage(Guid Id)
        {
            var toRemove = context.Messages
                    .Include(m => m.Commentaires)
                    .FirstOrDefault(m => m.Id == Id);

            asserts.Exists(toRemove, "Message not found.");
            asserts.IsOwnedByCurrentUser(toRemove, User);

            if (!User.IsInRole("Administrator") && toRemove.UserId.ToString() != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            {

                TempData["ErrorMessage"] = "Vous n'êtes pas autorisé à supprimer ce message.";

                return RedirectToAction(nameof(Index));
            }


            foreach (var commentaire in toRemove.Commentaires!)
            {
                if (commentaire.UserId.ToString() == User.FindFirstValue(ClaimTypes.NameIdentifier)
          || User.IsInRole("Administrator"))
                {
                    context.Commentaires?.Remove(commentaire);
                }
            }
            context.Messages.Remove(toRemove);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult RemoveCommentaire(Guid Id)
        {
            var toRemove = context.Commentaires.Find(Id);

            asserts.Exists(toRemove, "Message not found.");
            asserts.IsOwnedByCurrentUser(toRemove, User);


            if (!User.IsInRole("Administrator") && toRemove.UserId.ToString() != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            {
                TempData["ErrorMessage"] = "Vous n'êtes pas autorisé à supprimer ce message.";

                return RedirectToAction(nameof(Index));
            }



            context.Commentaires.Remove(toRemove);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}