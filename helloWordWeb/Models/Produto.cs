using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace helloWordWeb.Models
{
    public class Produto
    {
        [Key]
        public int Id {get; set; }

        [Required(ErrorMessage ="Este camo é de preenchimento obrigatório")]
        public string Name {get; set; } = string.Empty;
        [Required]
        [DisplayName("Preço do Produto")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal? Preco { get; set; } 

    }
}
