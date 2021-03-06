using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Care.Models
{
    public class ItemModel
    {
        [Key]
        public int ImageId { get; set; }

        [DisplayName("User ID")]
        public int UserId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("Item Name")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Image Name")]
        public string ImageName { get; set; }

        [Required]
        public string Condition { get; set; }

        [Required]
        public string Category { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
    }
}
