using System.ComponentModel.DataAnnotations.Schema;

namespace Tp5Messagerie.Entities
{
    public class Commentaire
    {

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid MessageID { get; set; }
        public string? Contenu { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser IdUser { get; set; }

        [ForeignKey(nameof(MessageID))]
        public virtual Message Messages { get; set; }



    }
}
