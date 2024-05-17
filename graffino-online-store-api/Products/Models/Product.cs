using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using graffino_online_store_api.OrderDetails.Models;

namespace graffino_online_store_api.Products.Models;

public abstract class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    
    [Required]  
    [Column("name")]
    public string Name { get; set; }
    
    [Required]
    [Column("price")]
    public double Price { get; set; }
    
    [Required]
    [Column("category")]
    public Category Category { get; set; }
    
    [Required]
    [Column("dateAdded")]
    public DateTime DateAdded { get; set; }
    
    [JsonIgnore]
    public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
}