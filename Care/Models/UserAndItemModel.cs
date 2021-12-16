using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Care.Models
{
    public class UserAndItemModel
    {
        [DisplayName("User ID")]
        public int UserId { get; set; }

        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        [DisplayName("Item Name")]
        public string ItemName { get; set; }

        [DisplayName("Image Name")]
        public string ImageName { get; set; }

        [DisplayName("Item Category")]
        public string ItemCategory { get; set; }

        [DisplayName("Item Condition")]
        public string ItemCondition { get; set; }

        public int ImageId { get; set; }
    }
}
