using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moura.painel.Model
{
    public class Painel_Portal_Empresa
    {
        public virtual int Codigo { get; set; }
        public virtual Empresa Empresa { get; set; }

        [JsonIgnore]
        public virtual Painel_Portal Portal { get; set; }
    }
}   
