using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using graffino_online_store_api.OrderDetails.Models;
using Microsoft.AspNetCore.Identity;

namespace graffino_online_store_api.Orders.Models;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    
    [Required]
    [Column("customerId")]
    public string CustomerId { get; set; }
    
    [Required]
    [Column("status")]
    public OrderStatus Status { get; set; }
    
    [Required]
    [Column("lastDateUpdated")]
    public DateTime? LastDateUpdated { get; set; }
    
    public virtual IdentityUser Customer { get; set; }
    public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
}