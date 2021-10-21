﻿using System;
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
                this.OrgLogo = post.OrgLogo;
                this.OrgLongDescr = post.OrgLongDescr;
                this.OrgName = post.OrgName;
                this.OrgPhoto = post.OrgPhoto;
                this.OrgShortDescr = post.OrgShortDescr;

                this.errorStatus = new ErrorStatus (null);
        }
    }
}
