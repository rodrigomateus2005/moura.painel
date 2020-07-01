using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using moura.painel.Model;
using moura.painel.Services;

namespace moura.painel.Controllers
{

    // [Authorize()]
    [Route("api/[controller]")]
    [ApiController]
    public class PainelController : ControllerBase
    {
        private IPainelRepository service { get; set; }
        private LoginTokenService tokenService { get; set; }
        private ILoginRepository loginService { get; set; }

        public PainelController(IPainelRepository service, ILoginRepository loginService, LoginTokenService tokenService, IHostingEnvironment hostingEnvironment)
        {
            this.service = service;
            this.tokenService = tokenService;
            this.loginService = loginService;

            var app = hostingEnvironment.ApplicationName;
        }

        // GET api/RetornarEmpresas
        [HttpGet("RetornarEmpresas")]
        public ActionResult<Painel_Portal[]> RetornarEmpresas()
        {
            var retorno = this.service.GetPortais();

            if (retorno?.Length > 0)
            {
                return retorno;
            }

            return new Painel_Portal[0];
        }

        // POST api/values
        [HttpPost("Login")]
        [AllowAnonymous()]
        public string Login([FromBody] Login login)
        {
            var loginValidos = this.loginService.Logar(login.Usuario, login.Senha);

            if (loginValidos != null)
            {
                return this.tokenService.GerarToken(loginValidos);
            }

            return null;
        }
    }
}
