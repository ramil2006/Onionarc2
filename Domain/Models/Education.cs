using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Education:BaseEntity
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public ICollection<Group> Groups { get; set; }
    }
}
