using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moura.painel.Model
{
    public class Empresa
    {

        public virtual int Codigo { get; set; }
        public virtual string Razao_Social { get; set; }
        public virtual string Fantasia { get; set; }

    }
}
