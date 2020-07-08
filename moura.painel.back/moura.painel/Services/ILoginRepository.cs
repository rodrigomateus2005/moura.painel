using moura.painel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moura.painel.Services
{
    public interface ILoginRepository
    {
        Login Logar(Painel_Portal[] paineis, string usuario, string senha);
    }
}
