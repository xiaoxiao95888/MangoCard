using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango_Cards.Library.Models
{
    public class LoginLog
    {
        [Key]
        public Guid Id { get; set; }
        public string State { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? WeChatUser_Id { get; set; }
        [ForeignKey("WeChatUser_Id")]
        public virtual WeChatUser WeChatUser { get; set; }
    }
}
