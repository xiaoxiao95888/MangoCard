using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MangoCard_Cards.Admin.Models.Enum;

namespace MangoCard_Cards.Admin.Models
{
    public class RegisterUserModel
    {
        public Guid ApplyForDeveloperId { get; set; }
        public Guid WeChatUserId { get; set; }
        public string NickName { get; set; }
        public Gender Gender { get; set; }
        public string Language { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Headimgurl { get; set; }
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

        public bool Pass { get; set; }
        /// <summary>
        /// 驳回
        /// </summary>
        public bool Reject { get; set; }
        public string RejectMessage { get; set; }
    }
}