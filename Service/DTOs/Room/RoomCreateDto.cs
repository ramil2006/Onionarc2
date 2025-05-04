using FluentValidation;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Room
{
    public class RoomCreateDto
    {
        public string Name { get; set; }
    }
    public class RoomCreateDtoValidator : AbstractValidator<RoomCreateDto>
    {
        public RoomCreateDtoValidator()
        {
            RuleFor(x=>x.Name).NotNull().NotEmpty().WithMessage("Name is required").Length(2,20);
        }
    }
}
