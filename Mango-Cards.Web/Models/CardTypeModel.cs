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
        public string Code { get; set; }
        public List<CardTypeModel> SubCardTypeModels { get; set; }
        public CardDemoModel[] CardDemoModels { get; set; }
        public MangoCardModel[] MangoCardModels { get; set; }
    }
}