using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace OnlineStore.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, MinLength(3, ErrorMessage = "Minimum Lenght is 3 symbols")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed")]
        public string Name { get; set; }
        [Required]
        public string Slug { get; set; }
        public int Sorting { get; set; }
    }
}
