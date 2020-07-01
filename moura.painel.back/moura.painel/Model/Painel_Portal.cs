using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moura.painel.Model
{
    public class Painel_Portal
    {
        public virtual int Codigo { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Link { get; set; }

        public virtual IEnumerable<Painel_Portal_Empresa> Empresas { get; set; }
    }
}
