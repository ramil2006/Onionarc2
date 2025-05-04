using AutoMapper;
using Domain.Models;
using Service.DTOs.Education;
using Service.DTOs.Group;
using Service.DTOs.Room;
using Service.DTOs.Student;
using Service.DTOs.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StudentCreateDto, Student>();
            CreateMap<Student, StudentDto>();
            CreateMap<StudentUpdateDto, Student>();
            CreateMap<TeacherCreateDto, Teacher>();
            CreateMap<Teacher, TeacherDto>();
            CreateMap<TeacherUpdateDto, Teacher>();
            CreateMap<EducationCreateDto, Education>();
            CreateMap<Education, EducationDto>();
            CreateMap<EducationUpdateDto, Education>();
            CreateMap<RoomCreateDto, Room>();
            CreateMap<Room, RoomDto>();
            CreateMap<RoomUpdateDto, Room>();
            CreateMap<GroupCreateDto, Group>();
            CreateMap<Group, GroupDto>();
            CreateMap<GroupUpdateDto, Group>();
        }
    }
}
