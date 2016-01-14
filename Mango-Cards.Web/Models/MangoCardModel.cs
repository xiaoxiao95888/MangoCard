using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mango_Cards.Web.Models.Enum;

namespace Mango_Cards.Web.Models
{
    public class MangoCardModel
    {      
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public Guid CardTypeId { get; set; }
        public PageType PageType { get; set; }
        public string HtmlCode { get; set; }
        /// <summary>
        /// 是否发布
        /// </summary>
        public bool IsPublish { get; set; }
        /// <summary>
        /// 浏览量
        /// </summary>
        public PvDataModel[] PvDataModels { get; set; }
        public int PvCount { get; set; }
        /// <summary>
        /// 分享次数
        /// </summary>
        public ShareTimeModel[] ShareTimeModels { get; set; }
        public int ShareTimeCount { get; set; }
        public OrderModel[] OrderModels { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}