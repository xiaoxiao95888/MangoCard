using System;

namespace Mango_Cards.Web.Models
{
    public class CompanyModel
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 微信APPID
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 微信AppSecret
        /// </summary>
        public string AppSecret { get; set; }
        public string Name { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}