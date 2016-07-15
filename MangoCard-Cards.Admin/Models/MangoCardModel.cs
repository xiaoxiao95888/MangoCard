using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MangoCard_Cards.Admin.Models
{
    public class MangoCardModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string HtmlCode { get; set; }
    }
}