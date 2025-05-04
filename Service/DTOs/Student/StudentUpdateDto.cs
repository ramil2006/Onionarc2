using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Service.DTOs.Student
{
    public class StudentUpdateDto
    {
        public int Id { get; set; }
        public string  FullName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public IFormFile UploadImage{ get; set; }
        public int GroupId { get; set; }

    }
    public class StudentUpdateDtoValidator : AbstractValidator<StudentUpdateDto>
    {
        public StudentUpdateDtoValidator()
        {
            RuleFor(x => x.FullName).NotNull().NotEmpty().WithMessage("Name is required.")
            .Length(2, 50).WithMessage("Name must be between 2 and 50 characters. ");


            RuleFor(x => x.Email).NotNull().WithMessage("Email not be null")
            .EmailAddress().WithMessage("Email format is wrong");

            RuleFor(x => x.GroupId).NotNull().NotEmpty().WithMessage("Group is required.");

        }
    }

}
