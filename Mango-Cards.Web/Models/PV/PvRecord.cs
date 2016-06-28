using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mango_Cards.Web.Models.PV
{
    public class PvRecord
    {
        public PvUser PvUser { get; set; }
        public Guid MangoCardId { get; set; }
    }
}