using System.ComponentModel.DataAnnotations;

namespace Care.Models
{
    public class OrgModel
    {
        [Required]
        [Key]
        public int OrgId { get; set; }
        [Required]
        public string OrgName { get; set; }
        public string OrgShortDescr { get; set; }
        [Required]
        public string OrgLongDescr { get; set; }
        public string OrgLink { get; set; }
        [Required]
        public string OrgLogo { get; set; }
        public string OrgPhoto { get; set; }

    }
}
