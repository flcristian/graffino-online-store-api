using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace graffino_online_store_api.Products.Models;

public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    
    [Required]  
    [Column("name")]
    public string Name { get; set; }
    
    public virtual List<Property> Properties { get; set; }
    
    [JsonIgnore]
    public virtual List<Product> Products { get; set; }
}