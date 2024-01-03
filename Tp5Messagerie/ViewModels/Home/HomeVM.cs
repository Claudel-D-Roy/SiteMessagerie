using Tp5Messagerie.Entities;
using Tp5Messagerie.Utilities;

namespace Tp5Messagerie.ViewModels.Home
{
    public class HomeVM
    {

        public Guid Id { get; set; }
        public Guid CommentaireID { get; set; }
     
        public DateTime CreatedDate { get; set; }
        public string Contenu { get; set; } 
        public PagingInfo PagingInfo { get; set; }
        public List<ApplicationUser> UserName { get; set; }
        public List<Message> Messages { get; set; }
    
    
    }
}
