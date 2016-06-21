using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models.Enum;

namespace Mango_Cards.Library.Models
{
    public class Field
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FieldValue { get; set; }
        public FieldType FieldType { get; set; }
        /// <summary>
        /// 页面字段根据Index排序显示
        /// </summary>
        public int? Index { get; set; }
        public Guid? MediaId { get; set; }
        [ForeignKey("MediaId")]
        public virtual Media Media { get; set; }
        public Guid? CardTemplateId { get; set; }
        [ForeignKey("CardTemplateId")]
        public virtual CardTemplate CardTemplate { get; set; }
        public Guid? MangoCardId { get; set; }
        [ForeignKey("MangoCardId")]
        public virtual MangoCard MangoCard { get; set; }
    }
}
