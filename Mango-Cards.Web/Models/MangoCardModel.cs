using System;
using System.Data.SqlTypes;
using System.Dynamic;
using System.Linq;
using System.Web.Helpers;
using Mango_Cards.Web.Models.Enum;

namespace Mango_Cards.Web.Models
{
    public class MangoCardModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ShareThumbnailUrl { get; set; }
        public Guid CardTypeId { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        public string Url { get; set; }
        public decimal? UnitPrice { get; set; }
        public DateTime UpdateTime { get; set; }
    }

    public class MangoCardAttributeModel
    {
        public Guid MangoCardId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MangoCardUrl { get; set; }
        /// <summary>
        /// 页面所需的field
        /// </summary>
        public FieldModel[] FieldModels { get; set; }
        public dynamic Field
        {
            get
            {
                if (FieldModels != null && FieldModels.Any())
                {
                    var customer = new ExpandoObject();
                    var dict = (System.Collections.Generic.IDictionary<String, Object>)customer;
                    foreach (var item in FieldModels)
                    {
                        dict.Add(item.Name, item.MediaModel != null ? item.MediaModel.Url : item.FieldValue);
                    }
                    return customer;
                }
                return "";
            }
        }
        public string HtmlCode { get; set; }
        public string Instructions { get; set; }
    }
}