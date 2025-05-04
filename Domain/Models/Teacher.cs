using Domain.Common;

namespace Domain.Models
{
    public class Teacher:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}
