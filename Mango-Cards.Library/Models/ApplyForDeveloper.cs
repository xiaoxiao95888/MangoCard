using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango_Cards.Library.Models
{
    public class ApplyForDeveloper : IDtStamped
    {
        public Guid Id { get; set; }
        public Guid WeChatUserId { get; set; }
        [ForeignKey("WeChatUserId")]
        public virtual WeChatUser WeChatUser { get; set; }
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
