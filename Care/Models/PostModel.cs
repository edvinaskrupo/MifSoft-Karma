using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Collections;
using Care.Helpers;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Care.Models
{
    public class PostModel : IEquatable<PostModel>, IComparable<PostModel>
    {
        [Required]
        [Key]
        public int OrgId { get; set; }
        [Required(ErrorMessage = "Enter organization name.")]
        public string OrgName { get; set; }
        public string OrgShortDescr { get; set; }
        [Required(ErrorMessage = "Enter organization description.")]
        public string OrgLongDescr { get; set; }
        public string OrgLink { get; set; }

        [DisplayName("Logo Name")]
        public string OrgLogoName { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Upload organization logo.")]
        [DisplayName("Logo File")]
        public IFormFile OrgLogoFile { get; set; }

        [DisplayName("Photo Name")]
        public string OrgPhotoName { get; set; }
        [NotMapped]
        [DisplayName("Photo File")]
        public IFormFile OrgPhotoFile { get; set; }


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

    public class PostModelWithErrorHandling : PostModel {
        public struct ErrorStatus {
            public readonly bool errorEncountered;
            public readonly string errorMessage;
            public ErrorStatus (string errorMessage) {
                this.errorEncountered = errorMessage != null;
                this.errorMessage = errorMessage;
            }
        }

        public readonly ErrorStatus errorStatus;

        public PostModelWithErrorHandling (string errorMessage) {
            if (errorMessage == null) {
                this.errorStatus = new ErrorStatus ("No error message provided.");
            }
            else {
                this.errorStatus = new ErrorStatus (errorMessage);
            }
        }
        public PostModelWithErrorHandling (PostModel post) {
                this.OrgId = post.OrgId;
                this.OrgLink = post.OrgLink;
                this.OrgLogoName = post.OrgLogoName;
                this.OrgLogoFile = post.OrgLogoFile;
                this.OrgLongDescr = post.OrgLongDescr;
                this.OrgName = post.OrgName;
                this.OrgPhotoName = post.OrgPhotoName;
                this.OrgPhotoFile = post.OrgPhotoFile;
                this.OrgShortDescr = post.OrgShortDescr;

                this.errorStatus = new ErrorStatus (null);
        }
    }
}
