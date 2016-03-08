using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models.Interfaces;

namespace Mango_Cards.Library.Models
{
    /// <summary>
    /// 每个页面的数据
    /// </summary>
    public class PageValue : IDtStamped
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Json格式数据
        /// </summary>
        public string Value { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
