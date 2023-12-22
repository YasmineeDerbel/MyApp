using System.ComponentModel.DataAnnotations;

namespace MyApp.Models
{
    public class Post
    {
        [Required]
        public string id { get; set; }  
        public string sujet { get; set; }
        
        [Required]
        public Utilisateur auteur { get; set; }
    }
}
