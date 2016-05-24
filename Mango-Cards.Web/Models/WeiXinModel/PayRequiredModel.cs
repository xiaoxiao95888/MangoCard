using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mango_Cards.Web.Models.WeiXinModel
{
    public class PayRequiredModel
    {
        public string appId { get; set; }
        public string mch_id { get; set; }
        public string nonce_str { get; set; }
        public string body { get; set; }
        public string out_trade_no { get; set; }
        public string total_fee { get; set; }
        public string spbill_create_ip { get; set; }
        public string notify_url { get; set; }
        public string trade_type { get; set; }
        public string sign { get; set; }
    }
}