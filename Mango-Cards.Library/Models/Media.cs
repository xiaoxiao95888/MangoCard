using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models.Interfaces;

namespace Mango_Cards.Library.Models
{
    public class Media : IDtStamped
    {
        public Guid Id { get; set; }
        public Guid WeChatUserId { get; set; }
        public Guid MediaTypeId { get; set; }
        /// <summary>
        /// 文件所属人
        /// </summary>
        [ForeignKey("WeChatUserId")]
        public virtual WeChatUser WeChatUser { get; set; }
        /// <summary>
        /// 原始文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 数据库文件名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 后缀
        /// </summary>
        public string ExtensionName { get; set; }
        [ForeignKey("MediaTypeId")]
        public virtual MediaType MediaType { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
