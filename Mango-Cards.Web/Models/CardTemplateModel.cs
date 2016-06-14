using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace Mango_Cards.Web.Models
{
    public class CardTemplateModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// 作者Id
        /// </summary>
        public Guid EmployeeId { get; set; }
        public string ThumbnailUrl { get; set; }
        public Guid CardTypeId { get; set; }
        public string CardTypeName { get; set; }
        public DateTime UpdateTime { get; set; }
    }
    public class CardTemplateDetailModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// 作者Id
        /// </summary>
        public Guid EmployeeId { get; set; }
        public string ThumbnailUrl { get; set; }
        public Guid CardTypeId { get; set; }
        public string CardTypeName { get; set; }
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
        public DateTime UpdateTime { get; set; }
    }
}