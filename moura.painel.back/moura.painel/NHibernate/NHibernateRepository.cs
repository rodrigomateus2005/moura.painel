using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moura.painel.NHibernate
{
    public abstract class NHibernateRepository
    {

        private static ISessionFactory sessionFactory;

        public NHibernateRepository()
        {
            if (NHibernateRepository.sessionFactory == null)
            {
                var mapper = new ModelMapper();
                mapper.AddMappings(this.GetType().Assembly.ExportedTypes.Where(x => x.Namespace == "moura.painel.NHibernate.Maps").ToArray());
                HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

                var configuration = new Configuration();
                configuration.DataBaseIntegration(c =>
                {
                    c.Dialect<MsSql2008Dialect>();
                    c.ConnectionString = this.GetConnectonString();
                    c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                    c.SchemaAction = SchemaAutoAction.Validate;
                    c.LogFormattedSql = true;
                    c.LogSqlInConsole = true;
                });
                configuration.AddMapping(domainMapping);

                NHibernateRepository.sessionFactory = configuration.BuildSessionFactory();
            }
        }

        private string GetConnectonString()
        {
            var conStrBuilder = new System.Data.SqlClient.SqlConnectionStringBuilder();

            conStrBuilder.DataSource = "cloud4.jnmoura.com.br,1495";
            conStrBuilder.InitialCatalog = "LUPO_GLOBAL";
            conStrBuilder.UserID = "userLUPO_GLOBAL_HML";
            conStrBuilder.Password = "Mour@505050";

            return conStrBuilder.ToString();
        }

        protected ISession OpenSession()
        {
            return NHibernateRepository.sessionFactory.OpenSession();
        }

    }
}
