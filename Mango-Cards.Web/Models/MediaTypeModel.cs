using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mango_Cards.Web.Models
{
    public class MediaTypeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public MediaModel[] MediaModels { get; set; }
    }
}