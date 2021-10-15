using System.Linq;
using Care.Models;

namespace Care.Helpers
{
    public class IndexRetriever
    {
        public InfoIndexViewModel retrieveIndex (ServiceDbContext context) {
            InfoIndexViewModel indexView = new InfoIndexViewModel {
                index = context.Posts.ToArray()
            };

            return indexView;
        }
    }
}
