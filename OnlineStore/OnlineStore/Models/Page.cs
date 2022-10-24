using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class Page
    {
        public int Id { get; set; }
        [Required, MinLength(3, ErrorMessage = "Minimum Lenght is 3 symbols")]
        public string Title { get; set; }
        public string Slug { get; set; }
        [Required, MinLength(4, ErrorMessage = "Minimum Lenght is 4 symbols")]
        public string Content { get; set; }
        public int Sorting { get; set; }
    }
}
