using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mango_Cards.Web.Models.Enum;
using MongoDB.Bson;

namespace Mango_Cards.Web.Models.PV
{
    public class PvUser
    {
        public ObjectId Id { get; set; }
        public string OpenId { get; set; }
        public string NickName { get; set; }
        public Sex Sex { get; set; }
        public string Language { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Headimgurl { get; set; }
    }
}