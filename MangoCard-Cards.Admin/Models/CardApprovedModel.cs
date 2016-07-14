using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MangoCard_Cards.Admin.Models
{
    public class CardApprovedModel
    {
        public Guid Id { get; set; }
        public Guid MangocardId { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }
}