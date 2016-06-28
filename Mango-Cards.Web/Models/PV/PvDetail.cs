using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace Mango_Cards.Web.Models.PV
{
    public class PvDetail
    {
        public ObjectId Id { get; set; }
        public string OpenId { get; set; }
        public Guid MangoCardId { get; set; }
        public DateTime ViewDate { get; set; }
    }
}