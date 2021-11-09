using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Persistence.Repositories;
using StudyDesck.API.Domain.Services;
using StudyDesck.API.Domain.Services.Comunications;
using StudyDesck.API.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net;

namespace StudyDesck.API.Services
{
    public class UserService: IUserService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITutorRepository _tutorRepository;
        private readonly ICourseRepository _courseRepository;

        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, IStudentRepository studentRepository, 
            ITutorRepository tutorRepository, ICourseRepository courseRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _appSettings = appSettings.Value;
            _studentRepository = studentRepository;
            _tutorRepository = tutorRepository;
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public AuthenticationResponse Authenticate(AuthenticationRequest request)
        {
            // finbyEmail
            AuthenticationResponse response;
            var students = _studentRepository.ListAsync();
            var student = students.Result.SingleOrDefault(x => x.Email == request.Email);
            if (student == null)
            {
                return new AuthenticationResponse("this email doesn't correspond to any user");
            }
            else
            {
                if (student.isTutor)
                {
                    var tutors = _tutorRepository.ListAsync();
                    var tutor = tutors.Result.SingleOrDefault(x => x.Email == request.Email);
                    if (!BCryptNet.BCrypt.Verify(request.Password, tutor.Password))
                    {
                        return new AuthenticationResponse("Invalid password for this user");
                    }
                    response = _mapper.Map<Tutor, AuthenticationResponse>(tutor);
                    response.IsTutor = true;
                }
                else
                {
                    if (!BCryptNet.BCrypt.Verify(request.Password, student.Password)) { 
                        return new AuthenticationResponse("Invalid password for this user");
                    }
                    
                    response = _mapper.Map<Student, AuthenticationResponse>(student);
                    response.IsTutor = false;
                }
                response.Token = GenerateJwtToken(response.Id.ToString());
                return response;

            }

            
        }

        public async Task<AuthenticationResponse> ConvertStudentToTutor(int studentId, int courseId) {
            var student = await _studentRepository.FindById(studentId);
            var course = await _courseRepository.FindById(courseId);

            if (student != null && course != null)
            {
                if (!student.isTutor)
                {
                    Tutor _tutor = new Tutor
                    {
                        Name = student.Name,
                        LastName = student.LastName,
                        CourseId = course.Id,
                        Logo = student.Logo,
                        Email = student.Email,
                        Password = student.Password,
                    };

                    try
                    {
                        await _tutorRepository.AddAsync(_tutor);
                        student.isTutor = true;
                        _studentRepository.Update(student);
                        await _unitOfWork.CompleteAsync();
                        return new AuthenticationResponse(null);
                    }
                    catch (Exception ex)
                    {
                        return new AuthenticationResponse($"An error ocurred while converting the student: {ex.Message}");
                    }

                }
                else
                    return new AuthenticationResponse("This student is already a tutor");


            }
            else {
                
                return new AuthenticationResponse("Student Id or Course Id not found");
            }

            
           
            
        }




        private string GenerateJwtToken(string value)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, value)
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        //public IEnumerable<User> GetAll()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
