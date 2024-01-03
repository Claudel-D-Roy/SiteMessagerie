using System.ComponentModel.DataAnnotations.Schema;

namespace Tp5Messagerie.Entities
{
    public class Message
    {

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CommentaireID { get; set; }
        public string? Contenu { get; set; }
        public DateTime CreatedDate { get; set; }


        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser UserCom { get; set; }

        [ForeignKey(nameof(CommentaireID))]
        public List<Commentaire>? Commentaires { get; set; }


    }
}
