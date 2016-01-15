using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web.Helpers;
using Mango_Cards.Web.Models.Enum;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

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
        /// <summary>
        /// 页面所需的field
        /// </summary>
        public FieldModel[] FieldModels { get; set; }
        /// <summary>
        /// 页面收集到的数据
        /// </summary>
        public PageValueModel[] PageValueModels { get; set; }
        public dynamic PageValues
        {
            get
            {
                var dy = PageValueModels.Select(item => Json.Decode(item.Value)).ToList();
                return dy;
            }
        }

        public dynamic Field
        {
            get
            {
                var customer = new ExpandoObject();
                var dict = (System.Collections.Generic.IDictionary<String, Object>)customer;
                foreach (var item in FieldModels)
                {
                    dict.Add(item.Name, item.FieldValue);
                }
                return customer;
            }
        }

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