using System;
using System.ComponentModel.DataAnnotations;
using Mango_Cards.Library.Models.Interfaces;

namespace Mango_Cards.Library.Models
{
    public class CardDemo : IDtStamped
    {
        [Key]
        public Guid Id { get; set; }
        public virtual CardType CardType { get; set; }
        public string Name { get; set; }
        public string HtmlCode { get; set; }
        public string ThumbnailUrl { get; set; }
        public virtual Employee Employee { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
