using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Projecto.Receitas.Domain.Model
{
    public class Endereco
    {
        public int EnderecoId { get; set; }
        public string Rua {  get; set; }
        public string CodigoPostal { get; set; }
        public string Localidade { get; set; }
        public string Pais {  get; set; }
        public List<Utilizador> Utilizadores { get; set; }

        public Endereco(string rua,string codigoPostal,string localidade,string pais) 
        { 
            ValidarRua(rua);
            Rua = rua;
            ValidarCodigoPostal(codigoPostal);
            CodigoPostal = codigoPostal;
            ValidarLocalidade(localidade);
            Localidade = localidade;
            ValidarPais(pais);
            Pais = pais;
        }

        public Endereco(int enderecoId, string rua, string codigoPostal, string localidade, string pais)
            :this(rua,codigoPostal,localidade,pais) 
        {
            EnderecoId = enderecoId;
        }

        private static void ValidarRua(string rua) 
        {
            if(string.IsNullOrEmpty(rua)) 
            {
                throw new ArgumentNullException("A rua não pode ser vazia.");
            }
            else if (rua.Length>300) 
            {
                throw new ArgumentNullException("A rua não pode ter mais de 300 caracteres.");
            }
        }
        private static void ValidarCodigoPostal(string codigoPostal)
        {
            if (string.IsNullOrEmpty(codigoPostal))
            {
                throw new ArgumentNullException("O código postal não pode ser vazio.");
            }
            else if (codigoPostal.Length>50)
            {
                throw new ArgumentNullException("O código postal não pode ter mais de 50 caracteres.");
            }
        }

        private static void ValidarLocalidade(string localidade)
        {
            if (string.IsNullOrEmpty(localidade))
            {
                throw new ArgumentNullException("A localidade não pode ser vazia.");
            }
            else if (localidade.Length>50)
            {
                throw new ArgumentNullException("A localidade não pode ter mais de 150 caracteres.");
            }
        }

        private static void ValidarPais(string pais)
        {
            if (string.IsNullOrEmpty(pais))
            {
                throw new ArgumentNullException("O país não pode ser vazio.");
            }
            else if (pais.Length>150)
            {
                throw new ArgumentNullException("O país não pode ter mais de 150 caracteres.");
            }
        }
    }
}
