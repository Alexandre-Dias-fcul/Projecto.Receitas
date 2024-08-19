using System.ComponentModel.DataAnnotations;

namespace Projecto.Receitas.WebApp.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O Username é obrigatório.")]
        [StringLength(250, ErrorMessage = "O username tem no máximo 250 caracteres.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "A Password é obrigatória.")]
        [StringLength(50,ErrorMessage="A password tem no máximo 50 caracteres.")]
        public string Password { get; set; }
    }
}
