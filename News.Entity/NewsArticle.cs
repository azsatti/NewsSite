using System;
using System.ComponentModel.DataAnnotations;

namespace News.Entity
{
    /// <summary>
    /// News Article model class
    /// </summary>
    public class NewsArticle
    {
        /// <summary>
        /// This is Id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// This is title and its mandatory, and should not be more than 200 char.
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        
        /// <summary>
        /// This is Body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// This is datePublished and its mandatory.
        /// </summary>
        [Required]
        public DateTime DatePublished { get; set; }

        /// <summary>
        /// This is author Id and its mandatory.
        /// </summary>
        [Required]
        public string AuthorName { get; set; }
    }
}
