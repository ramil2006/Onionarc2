using FluentValidation;

namespace Service.DTOs.Teacher
{
    public class TeacherCreateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
    public class TeacherCreateDtoValidator : AbstractValidator<TeacherCreateDto>
    {
        public TeacherCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is required.")
            .Length(2, 50).WithMessage("Name must be between 2 and 50 characters. ");


            RuleFor(x => x.Surname).NotNull().WithMessage("Email not be null");
        }
    }
}
