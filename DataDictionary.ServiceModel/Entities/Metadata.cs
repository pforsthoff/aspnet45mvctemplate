using DataDictionary.ServiceModel.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace DataDictionary.ServiceModel.Entities
{
    public class Metadata : EntityBase<int>
    {
        [Required]
        public string TableName { get; set; }
        public int Column { get; set; }
        public int Entity { get; set; }
        public string RecordingRate { get; set; }
   

    }
}
