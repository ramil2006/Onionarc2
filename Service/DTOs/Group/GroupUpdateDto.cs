using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Group
{
    public class GroupUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EducationId { get; set; }
        public int TeacherId { get; set; }
        public int RoomId { get; set; }
    }
    public class GroupUpdateDtoValidator : AbstractValidator<GroupUpdateDto>
    {
        public GroupUpdateDtoValidator()
        {
            RuleFor(g => g.Name).NotEmpty().WithMessage("Name is not be Empty");
            RuleFor(g => g.Name).NotNull().WithMessage("Name is not be Null");
            RuleFor(g => g.TeacherId).NotNull().NotEmpty();
            RuleFor(g => g.EducationId).NotEmpty().NotNull();
            RuleFor(g => g.RoomId).NotNull().NotEmpty();
        }
    }
}
