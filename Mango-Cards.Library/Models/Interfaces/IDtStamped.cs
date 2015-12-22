using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango_Cards.Library.Models.Interfaces
{
    public interface IDtStamped
    {
        DateTime? UpdateTime { get; set; }
        DateTime CreatedTime { get; set; }
        bool IsDeleted { get; set; }
    } 
}
