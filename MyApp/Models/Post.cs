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

        [Required]
        public int auteurid { get; set; }
        public ICollection<Response> Responses { get; set; } = new List<Response>();
        //public Utilisateur Auteur { get; set; }
        //public List<Response> Reponses { get; set; }


    }
}
