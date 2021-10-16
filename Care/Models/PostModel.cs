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

            // ...
        }

        /*IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }*/

        /*public PostsEnum GetEnumerator()
        {
            return new PostsEnum(_posts);
        }*/

    }
        /*public class PostsEnum : IEnumerator
        {
            public List<PostModel> _posts;
            int position = -1;

        public PostsEnum(List<PostModel> list)
        {
            _posts = list;
        }
        public bool MoveNext()
            {
                position++;
                return (position < _posts.Count);
            }

            public void Reset()
            {
                position = -1;
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public PostModel Current
            {
                get
                {
                    try
                    {
                        return _posts[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }*/
        
        
   // }

