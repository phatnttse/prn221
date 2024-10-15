using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Entities.Enums;

namespace BusinessObjects.Entities
{
    public class OrderDetail : BaseEntity
    {

        [ForeignKey("Order")]
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey("Seller")]
        public string SellerId { get; set; }
        public virtual Account Seller { get; set; }

        [ForeignKey("FlowerListing")]
        public string FlowerListingId { get; set; }
        public virtual FlowerListing FlowerListing { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [EnumDataType(typeof(OrderDetailStatus))]
        public OrderDetailStatus Status { get; set; }

        public bool IsDeleted { get; set; }
    }
}
