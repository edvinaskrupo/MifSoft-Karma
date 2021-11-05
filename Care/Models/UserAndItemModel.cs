using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Care.Models
{
    public class UserAndItemModel
    {
        public IEnumerable<ItemModel> Items { get; set; }
        public IEnumerable<UserModel> Users { get; set; }
    }
}
