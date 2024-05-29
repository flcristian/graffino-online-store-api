using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace graffino_online_store_api.Products.Models;

public class Property
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    
    [Required]  
    [Column("name")]
    public string Name { get; set; }
    
    [Required]  
    [Column("categoryId")]
    public int CategoryId { get; set; }
    
    [JsonIgnore]
    public virtual Category Category { get; set; }
    
    [JsonIgnore]
    public virtual List<ProductProperty> ProductProperties { get; set; } 
}