using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace graffino_online_store_api.Products.Models;

public class TV : Product
{
    [Required]  
    [Column("diameter")]
    public string Diameter { get; set; }
    
    [Required]  
    [Column("resolution")]
    public string Resolution { get; set; }
}