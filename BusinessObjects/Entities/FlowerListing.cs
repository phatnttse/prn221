using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Entities.Enums;

namespace BusinessObjects.Entities
{
    public class FlowerListing : BaseEntity
    {

        [ForeignKey("Seller")]
        public string SellerId { get; set; }

        public virtual Account Seller { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        public string EventType { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [StringLength(1000)]
        public string ImageUrl { get; set; }

        [EnumDataType(typeof(FlowerListingStatus))]
        public FlowerListingStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public string RejectReason { get; set; }

        [ForeignKey("Category")]
        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }

    }
}
