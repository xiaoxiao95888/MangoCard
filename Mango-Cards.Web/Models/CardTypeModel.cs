using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mango_Cards.Web.Models
{
    public class CardTypeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CardTemplateModel[] CardTemplateModels { get; set; }
    }
}