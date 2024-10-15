using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Entities.Enums;

namespace BusinessObjects.Entities
{
    public class Order : BaseEntity
    {

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual Account User { get; set; }

        [Required]
        [StringLength(100)]
        public string BuyerName { get; set; }

        [Required]
        [StringLength(50)]
        public string BuyerPhone { get; set; }

        [Required]
        [StringLength(100)]
        public string BuyerEmail { get; set; }

        [Required]
        public string BuyerAddress { get; set; }

        [Required]
        public string Note { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalPrice { get; set; }

        [Required]
        [EnumDataType(typeof(OrderStatus))]
        public OrderStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>(); // Navigation property cho OrderDetail

    }
}
