using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace graffino_online_store_api.Products.Models;

public class Clothing : Product
{
    [Required]  
    [Column("color")]
    public string Color { get; set; }
    
    [Required]  
    [Column("style")]
    public string Style { get; set; }
    
    [Required]  
    [Column("size")]
    public string Size { get; set; }
}