using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebScraperApi.Models
{
    public class DetailsForVisitor
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("CardBasicData")]
        public string tenderIdString { get; set; }
        public virtual CardBasicData? CardBasicData { get; set; }
    }
}
