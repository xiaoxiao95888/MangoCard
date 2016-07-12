using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models.Interfaces;

namespace Mango_Cards.Library.Models
{
    public class CardApproved : IDtStamped
    {
        public Guid Id { get; set; }
        public Guid MangoCardId { get; set; }
        [ForeignKey("MangoCardId")]
        public virtual MangoCard MangoCard { get; set; }
        /// <summary>
        /// 是否通过
        /// </summary>
        public bool? IsApproved { get; set; }
        public string Message { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
