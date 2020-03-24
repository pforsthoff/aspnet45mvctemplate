using DataDictionary.Data.Mappings;
using DataDictionary.ServiceModel.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;

namespace DataDictionary.Data
{
    public class DataDictionaryModelContext : DbContext
    {
        static DataDictionaryModelContext()
        {
            Database.SetInitializer<DataDictionaryModelContext>(null);
        }

        public DataDictionaryModelContext()
            : base("name=dev")
        {
            // Disable proxy creation as this messes up the data service
            Configuration.ProxyCreationEnabled = false;
            //Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Metadata> Aspects { get; set; }

        //TODO:  If we implement the AuditableEntity we will need to override the base SaveChanges() method.
        public override int SaveChanges()
        {
            var changeInfo = this.ChangeTracker.Entries();
            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.Log = (query) => Debug.Write(query);
            modelBuilder.Types().Configure
            (c => c.ToTable(c.ClrType.Name.ToUpper()));

            modelBuilder.Properties().Configure
                (c => c.HasColumnName(c.ClrPropertyInfo.Name.ToUpper()));

            modelBuilder.Configurations.Add(new MetadataMap());
           
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
