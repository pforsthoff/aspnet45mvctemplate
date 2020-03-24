using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DataDictionary.ServiceModel.Entities;

namespace DataDictionary.Data.Mappings
{
    public class MetadataMap : EntityTypeConfiguration<Metadata>
    {
        public MetadataMap()
        {
            // Table
            ToTable("Metadata");

            // Ignore Table Columns
            Ignore(t => t.Key);

            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Column Mappings
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.TableName).HasColumnName("TableName");
            Property(t => t.Column).HasColumnName("Column");
            Property(t => t.Entity).HasColumnName("Entity");
            Property(t => t.RecordingRate).HasColumnName("RecordingRate");
        }
    }
}
