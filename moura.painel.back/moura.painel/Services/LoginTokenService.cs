using Microsoft.AspNetCore.Authentication;
using moura.painel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace moura.painel.Services
{
    public class LoginTokenService
    {

        private static IDictionary<string, AuthenticateResult> autorizacoes;

        public LoginTokenService()
        {
            if (LoginTokenService.autorizacoes == null)
            {
                LoginTokenService.autorizacoes = new Dictionary<string, AuthenticateResult>();
            }
        }


        public AuthenticateResult GetAutenticacao(string bearer)
        {
            if (!LoginTokenService.autorizacoes.ContainsKey(bearer))
                return AuthenticateResult.NoResult();

            return LoginTokenService.autorizacoes[bearer];
        }

        public string GerarToken(Login login)
        {
            string hash;

            using (MD5 md5Hash = MD5.Create())
            {
                var id = DateTime.Now.Ticks;

                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(id.ToString()));

                //StringBuilder sBuilder = new StringBuilder();

                //for (int i = 0; i < data.Length; i++)
                //{
                //    sBuilder.Append(data[i].ToString("x2"));
                //}

                //return sBuilder.ToString();

                hash = Convert.ToBase64String(data);
            }

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, login.Usuario));
            var identity = new ClaimsIdentity(claims, "Basic");

            ClaimsPrincipal c = new ClaimsPrincipal(identity);
            AuthenticationTicket ticket = new AuthenticationTicket(c, "Custom Scheme");
            AuthenticateResult autenticacao = AuthenticateResult.Success(ticket);

            LoginTokenService.autorizacoes.Add(hash, autenticacao);

            return hash;
        }


    }
}
