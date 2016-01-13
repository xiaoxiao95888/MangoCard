using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mango_Cards.Web.Models
{
    public class MangoCardTypeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public MangoCardModel[] MangoCardModels { get; set; }
    }
}