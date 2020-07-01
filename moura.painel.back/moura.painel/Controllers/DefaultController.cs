using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace moura.painel.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            // this.Request.app

            var arquivo = "View" + this.Request.Path.Value;
            if (System.IO.File.Exists(arquivo))
            {
                var provider = new FileExtensionContentTypeProvider();
                string contentType;
                if (!provider.TryGetContentType(arquivo, out contentType))
                {
                    contentType = "application/octet-stream";
                }

                return this.File(System.IO.File.ReadAllBytes(arquivo), contentType);
            }

            var basePath = this.Request.PathBase.ToString();
            if (!basePath.EndsWith("/"))
            {
                basePath += "/";
            }
            var conteudoPagina = System.IO.File.ReadAllText("View/index.html");
            conteudoPagina = conteudoPagina?.Replace("<base href=\"/\">", $"<base href=\"{basePath}\">");

            return this.Content(conteudoPagina, "text/html");
        }
    }
}