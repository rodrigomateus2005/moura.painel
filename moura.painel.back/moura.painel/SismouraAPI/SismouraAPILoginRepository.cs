using moura.painel.Model;
using moura.painel.Services;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moura.painel.SismouraAPI
{
    public class SismouraAPILoginRepository : ILoginRepository
    {
        private RestClient client;

        public SismouraAPILoginRepository()
        {
            this.client = new RestClient("https://mourasite2.jnmoura.com.br/LUPO_GLOBAL");
        }

        public Login Logar(string usuario, string senha)
        {
            var login = new
            {
                usuario = usuario,
                senha = senha,
                loginWindows = false,
                empresaSelecionada = 0
            };

            var request = new RestRequest("Login/Login", DataFormat.Json);

            request.AddJsonBody(login);

            var response = client.Post<LoginRetorno>(request);

            Login retorno = null;

            if (response.IsSuccessful && response.Data != null)
            {
                var responseTyped = response.Data;

                retorno = new Login();
                retorno.Usuario = responseTyped.usuario.Abreviacao;
                retorno.HashServidor = responseTyped.token;
            }

            return retorno;
        }

        private class LoginRetorno
        {
            public UsuarioRetorno usuario { get; set; }
            public string token { get; set; }
        }

        private class UsuarioRetorno
        {
            public int Codigo { get; set; }
            public string Nome { get; set; }
            public string Abreviacao { get; set; }

        }
    }
}
