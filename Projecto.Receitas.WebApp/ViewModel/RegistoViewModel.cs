using Projecto.Receitas.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Projecto.Receitas.WebApp.ViewModel
{
    public class RegistoViewModel
    {
        [Required(ErrorMessage = "O primeiro nome é obrigatório")]
        [StringLength(150, ErrorMessage = "O primeiro nome pode ter no máximo 150 caracteres")]
        public string PrimeiroNome { get; set; }

        [Required(ErrorMessage = "O último nome é obrigatório.")]
        [StringLength(150, ErrorMessage = "O último nome pode ter no máximo 150 caracteres.")]
        public string UltimoNome { get; set; }
        [Required(ErrorMessage = "O Username é obrigatório.")]
        [StringLength(250, ErrorMessage = "O username tem no máximo 250 caracteres.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "A Password é obrigatória.")]
        [StringLength(50, ErrorMessage = "A password tem no máximo 50 caracteres.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "A Password é obrigatória.")]
        [StringLength(50, ErrorMessage = "A password tem no máximo 50 caracteres.")]
        [Compare("Password",ErrorMessage = "As passwords não coincidem.")]
        public string ConfirmPassword { get; set; }
    }
}
