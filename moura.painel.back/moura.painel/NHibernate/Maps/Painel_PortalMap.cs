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
    public class Painel_PortalMap : ClassMapping<Painel_Portal>
    {

        public Painel_PortalMap()
        {
            Table("Painel_Portal");

            Id(x => x.Codigo, x =>
            {
                x.Column("Codigo");
                x.Generator(Generators.Identity);
                x.Type(NHibernateUtil.Int32);

            });

            Property(b => b.Nome, x =>
            {
                x.Column("Nome");
                x.Length(50);
                x.Type(NHibernateUtil.AnsiString);
            });

            Property(b => b.Link, x =>
            {
                x.Column("Link");
                x.Length(255);
                x.Type(NHibernateUtil.AnsiString);
            });

            //Bag(x => x.Empresas, map =>
            //{
            //    // map.Inverse(true);
            //    map.Key(key => key.Column("Portal"));
            //    map.Lazy(CollectionLazy.NoLazy);
            //    map.Cascade(Cascade.None);
            //    map.Fetch(CollectionFetchMode.Select);

            //}, rel =>
            //{
            //    rel.OneToMany();
            //});
        }

    }
}
