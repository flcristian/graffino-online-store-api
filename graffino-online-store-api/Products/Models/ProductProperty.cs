using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace graffino_online_store_api.Products.Models;

public class ProductProperty
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    
    [Required]  
    [Column("productId")]
    public int ProductId { get; set; }
    
    [Required]  
    [Column("propertyId")]
    public int PropertyId { get; set; }
    
    [Required]
    [Column("value")]
    public string Value { get; set; }
    
    [JsonIgnore]
    public virtual Product Product { get; set; } 
    
    public virtual Property Property { get; set; } 
}