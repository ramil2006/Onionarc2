using FluentValidation;

namespace Service.DTOs.Room
{
    public class RoomUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class RoomupdateDtoValidator : AbstractValidator<RoomUpdateDto>
    {
        public RoomupdateDtoValidator()
        {
            RuleFor(r => r.Name).NotEmpty().NotNull().WithMessage("Name is required");
            RuleFor(r => r.Name).Length(2, 20).WithMessage("Min length 2, Max length 20");
        }
    }
}
