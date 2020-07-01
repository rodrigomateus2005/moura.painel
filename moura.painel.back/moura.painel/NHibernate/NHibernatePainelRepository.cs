using moura.painel.Model;
using moura.painel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using NHibernate.Linq;

namespace moura.painel.NHibernate
{
    public class NHibernatePainelRepository : NHibernateRepository, IPainelRepository
    {
        public NHibernatePainelRepository() : base()
        {
        }

        public Painel_Portal[] GetPortais()
        {
            using (var session = this.OpenSession())
            {
                var retorno = session.Query<Painel_Portal>().ToArray(); // .FetchMany(x => x.Empresas).ToArray();
                return retorno;
            }
        }
    }
}
