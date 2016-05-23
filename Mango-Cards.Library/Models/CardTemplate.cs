using Mango_Cards.Library.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango_Cards.Library.Models
{
    public class CardTemplate : IDtStamped
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// 模板编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 是否发布
        /// </summary>
        public bool IsPublish { get; set; }
        /// <summary>
        /// 说明文档
        /// </summary>
        public string Instructions { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public virtual Employee Employee { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string HtmlCode { get; set; }
        public string ThumbnailUrl { get; set; }
        public virtual CardType CardType { get; set; }
        public virtual ICollection<Field> Fields { get; set; }
        public virtual ICollection<MangoCard> MangoCards { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
