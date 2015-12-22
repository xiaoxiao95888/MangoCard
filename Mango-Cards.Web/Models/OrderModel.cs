using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mango_Cards.Web.Models
{
    public class OrderModel
    {
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
        public EmployeeModel EmployeeModel { get; set; }
        public MangoCardModel MangoCardModel { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}