using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using graffino_online_store_api.Orders.Models;
using graffino_online_store_api.Products.Models;

namespace graffino_online_store_api.OrderDetails.Models;

public class OrderDetail
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    
    [Required]
    [Column("orderId")]
    public int OrderId { get; set; }
    
    [Required]
    [Column("productId")]
    public int ProductId { get; set; }
    
    [Required]
    [Column("count")]
    public int Count { get; set; }
    
    [JsonIgnore]
    public virtual Order Order { get; set; }
    
    public virtual Product Product { get; set; }
    
}