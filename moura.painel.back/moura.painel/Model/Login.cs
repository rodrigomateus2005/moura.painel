using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moura.painel.Model
{
    public class Login
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public Painel_Portal[] Paineis { get; set; }
    }
}
