using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models.Interfaces;

namespace Mango_Cards.Library.Models
{
    public class Order : IDtStamped
    {
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// 需求描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 工时
        /// </summary>
        public double? HourOfWork { get; set; }
        public DateTime? DeadLine { get; set; }
        /// <summary>
        /// 是否接单
        /// </summary>
        public bool IsOrderReceiving { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual MangoCard MangoCard { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
