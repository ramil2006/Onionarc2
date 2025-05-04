using Domain.Common;

namespace Domain.Models
{
    public class Group:BaseEntity
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.Now;
        public int EducationId { get; set; }
        public Education Education { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<Student> Students { get; set; }

    }
}
