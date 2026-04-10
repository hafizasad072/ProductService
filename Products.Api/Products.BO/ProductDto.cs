using System.ComponentModel.DataAnnotations;

namespace Products.BO
{
    public class CreateProductDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string Colour { get; set; } = string.Empty;

        [Range(0.01, 100000)]
        public decimal Price { get; set; }
    }
}
