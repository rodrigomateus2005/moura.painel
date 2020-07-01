using moura.painel.Model;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moura.painel.NHibernate.Maps
{
    public class Painel_Portal_EmpresaMap : ClassMapping<Painel_Portal_Empresa>
    {

        public Painel_Portal_EmpresaMap()
        {
            Table("Painel_Portal_Empresa");

            Id(x => x.Codigo, x =>
            {
                x.Column("Codigo");
                x.Generator(Generators.Identity);
                x.Type(NHibernateUtil.Int32);

            });

            ManyToOne(
            x => x.Empresa,
            map =>
            {
                map.Column("Empresa");
                map.NotNullable(true);
                map.NotFound(NotFoundMode.Ignore);
                map.Fetch(FetchKind.Join);
                map.ForeignKey("none");
                map.Lazy(LazyRelation.NoLazy);
            });

            ManyToOne(
            x => x.Portal,
            map =>
            {
                map.Column("Portal");
                map.Fetch(FetchKind.Join);
                map.ForeignKey("none");
                map.Lazy(LazyRelation.NoLazy);
            });
        }

    }
}
