using Azure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Models
{
    public class Post
    {
        [Required]
        public int id { get; set; }  
        public string sujet { get; set; }
        public string contenuSujet { get; set; }

        [Required]
        public int auteurid { get; set; }
        
        public ICollection<Response> Responses { get; set; } 
      

    }
}
