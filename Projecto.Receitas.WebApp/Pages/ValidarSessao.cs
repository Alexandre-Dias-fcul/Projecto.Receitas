using System.ComponentModel.Design;

namespace Projecto.Receitas.WebApp.Pages
{
    public class ValidarSessao
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidarSessao(IHttpContextAccessor httpContextAccessor) 
        { 
            _httpContextAccessor = httpContextAccessor;
        }
        public bool Validar(string codigo) 
        {
            var httpContext = _httpContextAccessor.HttpContext;

            Encriptacao.Key = Convert.FromBase64String(httpContext.Session.GetString("Key"));
            Encriptacao.IV = Convert.FromBase64String(httpContext.Session.GetString("IV"));
           
            int valorDescodificado=-1;

            try
            {
                byte[] stringEnviadaBinario = Convert.FromBase64String(codigo);
                valorDescodificado = Encriptacao.Descriptar(stringEnviadaBinario);
            }
            catch (Exception e)
            {
                return false;
            }

            if (httpContext.Session.GetInt32("UserId") == valorDescodificado) 
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        public string Encriptar(int valorOriginal) 
        {
            var httpContext = _httpContextAccessor.HttpContext;

            Encriptacao.Key = Convert.FromBase64String(httpContext.Session.GetString("Key"));
            Encriptacao.IV = Convert.FromBase64String(httpContext.Session.GetString("IV"));

            byte[] valorCodificado = Encriptacao.Encriptar(valorOriginal);

            return Convert.ToBase64String(valorCodificado);
        }
    }
}
