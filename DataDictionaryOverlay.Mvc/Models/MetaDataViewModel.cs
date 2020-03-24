using System.ComponentModel;

namespace DataDictionaryOverlay.Models
{
    public class MetadataViewModel
    {
        [DisplayName("Table Name")]
        public string TableName { get; set; }
        public int Column { get; set; }
        public int Entity { get; set; }
        [DisplayName("Recording Rate")]
        public string RecordingRate { get; set; }
        [DisplayName("Actions")]
        public string Actions { get; set; }
    }
}