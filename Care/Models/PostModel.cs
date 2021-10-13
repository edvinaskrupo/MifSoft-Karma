using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Care.Models
{
    public class PostModel
    {
        [Key]
        public int OrgId { get; set; }
        [Required]
        [DisplayName("Organization Name")]
        public string OrgName { get; set; }
        [DisplayName("Short Organization Description")]
        public string OrgShortDescr { get; set; }
        [Required]
        [DisplayName("Long Organization Description")]
        public string OrgLongDescr { get; set; }
        [DisplayName("Organization Link")]
        public string OrgLink { get; set; }
        [Required]
        [DisplayName("Organization Logo")]
        public string OrgLogo { get; set; }
        [DisplayName("Organization Photo")]
        public string OrgPhoto { get; set; }
    }
}
