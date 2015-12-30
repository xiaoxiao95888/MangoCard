using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mango_Cards.Web.Models
{
    public class CardDemoModel
    {
        public Guid Id { get; set; }
        public Guid CardTypeId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string HtmlCode { get; set; }
        public string ThumbnailUrl { get; set; }
        public Guid? EmployeeId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}