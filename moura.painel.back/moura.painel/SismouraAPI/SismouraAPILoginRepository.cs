using moura.painel.Model;
using moura.painel.Services;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        public Login Logar(Painel_Portal[] paineis, string usuario, string senha)
        {
            var login = new
            {
                usuario = usuario,
                senha = senha,
                loginWindows = false,
                empresaSelecionada = 0
            };

            var tasks = new List<Task<LoginRetorno>>();
            foreach (var portal in paineis)
            {
                tasks.Add(this.RequestLogin(portal, usuario, senha));
            }

            var retornos = Task.WhenAll(tasks.ToArray()).Result.Where(x => x != null).ToArray();

            if (retornos.Length <= 0)
            {
                return null;
            }

            var retorno = new Login();

            retorno.Usuario = usuario;
            retorno.Senha = senha;
            retorno.Paineis = retornos.Select(x => x.painel).ToArray();

            return retorno;
        }

        private async Task<LoginRetorno> RequestLogin(Painel_Portal painel, string usuario, string senha)
        {
            return await Task.Run(() =>
            {
                var login = new
                {
                    login = usuario,
                    senha = senha,
                    loginWindows = false,
                    windowWidth = 0,
                    windowHeight = 0
                };

                var client = new RestClient(painel.Link);

                var request = new RestRequest("Login/PreLogin", DataFormat.Json);

                request.AddJsonBody(login);

                var response = client.Post<LoginRetorno>(request);

                if (response.IsSuccessful && response.Data != null)
                {
                    response.Data.painel = painel;

                    if (response.Data.EmpresasPreLogin != null)
                    {
                        var empresas = new List<Painel_Portal_Empresa>();
                        foreach (var emp in response.Data.EmpresasPreLogin)
                        {
                            empresas.Add(new Painel_Portal_Empresa()
                            {
                                Codigo = emp.Codigo,
                                Empresa = new Empresa()
                                {
                                    Codigo = emp.Codigo,
                                    Fantasia = emp.Fantasia,
                                    Razao_Social = emp.Razao_Social
                                }
                            });
                        }
                        painel.Empresas = empresas.ToArray();
                    }
                    else
                    {
                        var empresas = new List<Painel_Portal_Empresa>();
                        empresas.Add(new Painel_Portal_Empresa()
                        {
                            Codigo = 0,
                            Empresa = new Empresa()
                            {
                                Codigo = 0,
                                Fantasia = "Portal",
                                Razao_Social = "Portal"
                            }
                        });
                        painel.Empresas = empresas.ToArray();
                    }

                    return response.Data;
                }
                else
                {
                    return null;
                }
            });
        }

        private class LoginRetorno
        {
            public UsuarioRetorno Usuario { get; set; }
            public EmpresaPreLogin[] EmpresasPreLogin { get; set; }
            public Painel_Portal painel { get; set; }
        }

        private class EmpresaPreLogin
        {
            public int Codigo { get; set; }
            public string Razao_Social { get; set; }
            public string Fantasia { get; set; }
            public bool Padrao { get; set; }
        }

        private class UsuarioRetorno
        {
            public int Codigo { get; set; }
            public string Nome { get; set; }
            public string Abreviacao { get; set; }

        }
    }
}
