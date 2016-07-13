using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mango_Cards.Web.Models
{
    public class ApplyForDeveloperModel
    {
        public Guid Id { get; set; }
        public Guid WeChatUserId { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNum { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 自我介绍
        /// </summary>
        public string Introduce { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}