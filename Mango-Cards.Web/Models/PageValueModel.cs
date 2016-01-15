using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mango_Cards.Web.Models
{
    public class PageValueModel
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}