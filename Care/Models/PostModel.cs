using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Collections;

namespace Care.Models
{
    public class PostModel 
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

    public class Posts : IEnumerable<PostModel>
    {
        private readonly PostModel[] array;

        public Posts(PostModel[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            this.array = array;
        }

        public int IndexOf(IEnumerable<PostModel> source, PostModel value)
        {
            int index = 0;
            var comparer = EqualityComparer<PostModel>.Default; 
            foreach (PostModel item in source)
            {
                if (comparer.Equals(item, value)) return index;
                index++;
            }
            return -1;
        }

        public IEnumerator<PostModel> GetEnumerator() => new PostsEnum(array);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
        public class PostsEnum : IEnumerator<PostModel>
        {
            private readonly PostModel[] array;

            public PostsEnum(PostModel[] array)
            {
                this.array = array;
            }

            private int index = -1;

            public PostModel Current => index >= 0 && index < array.Length ? array[index] : default(PostModel);

            object IEnumerator.Current => Current;

        public bool MoveNext()
            {
                index++;
                return index < array.Length;
            }

            public void Reset()
            {
                index = -1;
            }

            public void Dispose()
            { }
    }

    }