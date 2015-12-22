using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models.Interfaces;

namespace Mango_Cards.Library.Models
{
    public class Company : IDtStamped
    {
        [Key]
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
