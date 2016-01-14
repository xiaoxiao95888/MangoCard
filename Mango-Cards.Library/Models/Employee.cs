using Mango_Cards.Library.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango_Cards.Library.Models
{
    /// <summary>
    /// 雇员
    /// </summary>
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }
        public Gender Gender { get; set; }
        public string Name { get; set; }
        public string ThumbnailUrl { get; set; }
        public ICollection<MangoCard> MangoCards { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
