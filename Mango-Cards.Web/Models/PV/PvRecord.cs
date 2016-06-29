using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mango_Cards.Web.Models.PV
{
    public class PvRecord
    {
        public ObjectId Id { get; set; }
        public Guid MangoCardId { get; set; }
        public DateTime DateTime { get; set; }
        public PvUser PvUser { get; set; }
    }
}