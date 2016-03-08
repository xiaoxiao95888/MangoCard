using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mango_Cards.Library.Models.Enum;
using Mango_Cards.Library.Models.Interfaces;

namespace Mango_Cards.Library.Models
{
    public class WeChatUser : IDtStamped
    {
        [Key]
        public Guid Id { get; set; }
        public string OpenId { get; set; }
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
        public virtual ICollection<MangoCard> MangoCards { get; set; }
        public virtual ICollection<Media> Mediae { get; set; } 
        public virtual Company Company { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
