using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mango_Cards.Web.Models
{
    public class LoginLogModel
    {
        public Guid Id { get; set; }
        public string State { get; set; }
        public DateTime CreateTime { get; set; }
        public WeChatUserModel WeChatUserModel { get; set; }
    }
}