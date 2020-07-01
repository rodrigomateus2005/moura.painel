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
    public class EmpresaMap : ClassMapping<Empresa>
    {

        public EmpresaMap()
        {
            Table("Empresa");

            Id(x => x.Codigo, x =>
            {
                x.Column("Codigo");
                x.Generator(Generators.Assigned);
                x.Type(NHibernateUtil.Int16);

            });

            Property(b => b.Razao_Social, x =>
            {
                x.Column("Razao_Social");
                x.Length(100);
                x.Type(NHibernateUtil.AnsiString);
            });

            Property(b => b.Fantasia, x =>
            {
                x.Column("Fantasia");
                x.Length(100);
                x.Type(NHibernateUtil.AnsiString);
            });
        }

    }
}
