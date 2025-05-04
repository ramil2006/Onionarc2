using AutoMapper;
using Azure.Core;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Repository.Repositories.Interfaces;
using Service.DTOs.Student;

namespace Service.Services.Interfaces
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public StudentService(IStudentRepository studentRepository,
                              IMapper mapper,
                              IWebHostEnvironment env)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _env = env;
        }



        public async Task CreateAsync(StudentCreateDto student)
        {
            if (student.UploadImage == null || student.UploadImage.Length == 0)
            {
                throw new Exception("Image is required.");
            }
            if (student.UploadImage.Length > 1024 * 1024) 
            {
                throw new Exception("Image size must not be greater than 1MB.");
            }
            var fileName = $"{Guid.NewGuid()}-{student.UploadImage.FileName}";
            var filePath = Path.Combine(_env.WebRootPath, "images", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await student.UploadImage.CopyToAsync(stream);
            }
            var data = _mapper.Map<Student>(student);
            data.Image = fileName;
            await _studentRepository.CreateAsync(data);
        }


        public async Task DeleteAsync(int id)
        {
            var data = await _studentRepository.GetByIdAsync(id);

            if (data == null)
            {
                throw new Exception("Student not found.");
            }

            var filePath = Path.Combine(_env.WebRootPath, "images", data.Image);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            await _studentRepository.DeleteAsync(id);
        }


        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<StudentDto>>(await _studentRepository.GetAllAsync());
        }

        public async Task<StudentDto> GetByIdAsync(int id)
        {
            return _mapper.Map<StudentDto>(await _studentRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<StudentDto>> Search(string text)
        {
            return _mapper.Map<IEnumerable<StudentDto>>(await _studentRepository.Search(text));
        }
        public async Task<IEnumerable<StudentDto>> SortByAge(string sort)
        {
            return _mapper.Map<IEnumerable<StudentDto>>(await _studentRepository.SortByAge(sort));
        }
        public async Task UpdateAsync(StudentUpdateDto student)
        {
            var data = await _studentRepository.GetByIdAsync(student.Id);

            if (data == null)
            {
                throw new Exception("Student not found.");
            }

            if (student.UploadImage is not null)
            {
                if (student.UploadImage.Length > 900 * 1024)
                    throw new Exception("Image size must not be greater than 900 KB.");

                var oldImagePath = Path.Combine(_env.WebRootPath, "images", data.Image);
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }

                var fileName = $"{Guid.NewGuid()}-{student.UploadImage.FileName}";
                var newImagePath = Path.Combine(_env.WebRootPath, "images", fileName);

                using (var stream = new FileStream(newImagePath, FileMode.Create))
                {
                    await student.UploadImage.CopyToAsync(stream);
                }

                data.Image = fileName;
            }
            _mapper.Map(student, data);

            await _studentRepository.UpdateAsync(data);
        }


    }
}
