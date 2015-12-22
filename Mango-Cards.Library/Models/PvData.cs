using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models.Interfaces;

namespace Mango_Cards.Library.Models
{
    /// <summary>
    /// 浏览次数
    /// </summary>
    public class PvData:IDtStamped
    {
        [Key]
        public Guid Id { get; set; }
        public virtual WeChatUser WeChatUser { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
