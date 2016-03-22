using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango_Cards.Library.Models
{
    public class MediaType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 后缀
        /// </summary>
        public string Extension { get; set; }
        public virtual ICollection<Media> Mediae { get; set; } 
    }
}
