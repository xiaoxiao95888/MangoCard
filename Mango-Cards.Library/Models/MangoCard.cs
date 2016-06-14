using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Mango_Cards.Library.Models.Enum;
using Mango_Cards.Library.Models.Interfaces;
using System.Data.Entity.ModelConfiguration;

namespace Mango_Cards.Library.Models
{
    public class MangoCard : IDtStamped
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? CardTemplateId { get; set; }
        [ForeignKey("CardTemplateId")]
        public virtual CardTemplate CardTemplate { get; set; }
        public virtual ICollection<Field> Fields { get; set; }
        /// <summary>
        /// 页面收集到的数据
        /// </summary>
        public virtual ICollection<PageValue> PageValues { get; set; }        
        public string ThumbnailUrl { get; set; }
        /// <summary>
        /// share出去的缩略图
        /// </summary>
        public string ShareThumbnailUrl { get; set; }
        public Guid? WeChatUserId { get; set; }
        /// <summary>
        /// 持有人
        /// </summary>
        [ForeignKey("WeChatUserId")]
        public virtual WeChatUser WeChatUser { get; set; }
        public string HtmlCode { get; set; }
        /// <summary>
        /// 是否发布
        /// </summary>
        public bool IsPublish { get; set; }
        /// <summary>
        /// 浏览量
        /// </summary>
        public virtual ICollection<PvData> PvDatas { get; set; }
        /// <summary>
        /// 分享次数
        /// </summary>
        public virtual ICollection<ShareTime> ShareTimes { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class MangoCardMapping : EntityTypeConfiguration<MangoCard>
    {
        public MangoCardMapping()
        {
            HasMany(c => c.Fields)
                .WithOptional(p => p.MangoCard)
                .HasForeignKey(p => p.MangoCardId);

        }
    }
}
