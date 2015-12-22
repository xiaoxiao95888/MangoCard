using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models.Interfaces;

namespace Mango_Cards.Library.Models
{
    public class CardReview : IDtStamped
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 是否被退回
        /// </summary>
        public bool IsReturn { get; set; }
        public string Comments { get; set; } 
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
