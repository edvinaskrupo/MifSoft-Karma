using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Collections;
using Care.Helpers;
using Care.Controllers;

namespace Care.Models
{
    public class PostModel : IEquatable<PostModel>, IComparable<PostModel>
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


        public override bool Equals(object obj) =>
        (obj is PostModel post)
                ? Equals(post)
                : false;

        public int SortByNameAscending(string name1, string name2) => name1?.CompareTo(name2) ?? 1;

        public int CompareTo(PostModel comparePost) => comparePost == null ? 1 : OrgName.CompareTo(comparePost.OrgName);

        public bool Equals(PostModel other) => other is null ? false : OrgName.Equals(other.OrgName);

    }

    public class Posts<T> : IEnumerable<T>
    {
        private readonly T[] array;
        
        public Posts(T[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            this.array = array;
        }

        public int IndexOf(IEnumerable<T> source, T value)
        {
            int index = 0;
            var comparer = EqualityComparer<T>.Default; 
            foreach (T item in source)
            {
                if (comparer.Equals(item, value)) return index;
                index++;
            }
            return -1;
        }

        public IEnumerator<T> GetEnumerator() => new PostsEnum<T>(array);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
        public class PostsEnum<T> : IEnumerator<T>
        {
            private readonly T[] array;

            public PostsEnum(T[] array)
            {
                this.array = array;
            }

            private int index = -1;

            public T Current => index >= 0 && index < array.Length ? array[index] : default(T);

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

    public class PostModelError {
        public class ErrorStatus {
            private bool errorEncountered {get; set;}
            private string errorMessage {get; set;}
            public void SetError (string errorMessage) {
                this.errorEncountered = errorMessage != null;
                this.errorMessage = errorMessage;
            }

            public bool ErrorExists () {
                return this.errorEncountered;
            }
            public string GetErrorMessage () {
                return this.errorMessage;
            }
        }

        public static ErrorStatus errorStatus = new ErrorStatus();

        public PostModelError(InfoController publisher) {
            publisher.SetErrorEvent += delegate (object sender, EventArgsWithErrorMessage e) {
                lock (errorStatus) {
                    if (e.errorMessage == null) {
                        errorStatus.SetError ("No error message provided.");
                    }
                    else {
                        errorStatus.SetError (e.errorMessage);
                    }
                }
            };

            publisher.ResetErrorEvent += delegate (object sender, EventArgs e) {
                lock (errorStatus) {
                    errorStatus.SetError (null);
                }
            };
            
        }
    }
}
