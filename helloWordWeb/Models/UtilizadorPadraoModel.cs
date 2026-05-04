

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace helloWordWeb.Models
{
    public class UtilizadorPadraoModel : IdentityUser
    {
        [Required]
        public string Nome { get; set; }
        public string Morada { get; set; }
    }
}
