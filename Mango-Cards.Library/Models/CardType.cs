using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango_Cards.Library.Models
{
    public  class CardType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual CardType Parent { get; set; }
        public virtual ICollection<CardTemplate> CardTemplates { get; set; }
    }
}
