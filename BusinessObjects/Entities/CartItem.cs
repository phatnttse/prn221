using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Entities
{
    public class CartItem : BaseEntity
    {

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual Account User { get; set; }

        [ForeignKey("FlowerListing")]
        public string FlowerListingId { get; set; }
        public virtual FlowerListing FlowerListing { get; set; }

        [Required]
        public int Quantity { get; set; }

    }
}
