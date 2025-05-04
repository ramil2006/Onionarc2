using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Teacher
{
    public class TeacherUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
    public class TeacherUpdateDtoValidator : AbstractValidator<TeacherUpdateDto>
    {
        public TeacherUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is required.")
            .Length(2, 50).WithMessage("Name must be between 2 and 50 characters. ");


            RuleFor(x => x.Surname).NotNull().WithMessage("Email not be null");
        }
    }
}
