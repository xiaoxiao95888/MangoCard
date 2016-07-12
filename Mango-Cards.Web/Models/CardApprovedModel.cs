using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mango_Cards.Web.Models
{
    public class CardApprovedModel
    {
        public Guid Id { get; set; }
        public MangoCardModel MangoCardModel { get; set; }
        /// <summary>
        /// 是否通过
        /// </summary>
        public bool? IsApproved { get; set; }
        public string Message { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}