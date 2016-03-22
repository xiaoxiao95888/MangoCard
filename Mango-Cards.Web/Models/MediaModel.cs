using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mango_Cards.Web.Models
{
    public class MediaModel
    {
        public Guid Id { get; set; }
        public Guid MediaTypeId { get; set; }
        /// <summary>
        /// 原始文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 数据库文件名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 文件URL
        /// </summary>
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
        /// <summary>
        /// 后缀
        /// </summary>
        public string ExtensionName { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}