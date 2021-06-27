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
        private readonly ITutorRepository _tutorRepository;

        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, IStudentRepository studentRepository, ITutorRepository tutorRepository, IMapper mapper)
        {
            _appSettings = appSettings.Value;
            _studentRepository = studentRepository;
            _tutorRepository = tutorRepository;
            _mapper = mapper;
        }

        public AuthenticationResponse Authenticate(AuthenticationRequest request)
        {
            // finbyEmail
            AuthenticationResponse response;
            var students = _studentRepository.ListAsync();
            var student = students.Result.SingleOrDefault(x => x.Email == request.Email);

            if (student == null || !BCryptNet.BCrypt.Verify(request.Password, student.Password))
            {
                var tutors = _tutorRepository.ListAsync();
                var tutor = tutors.Result.SingleOrDefault(x => x.Email == request.Email);
                if (tutor == null || !BCryptNet.BCrypt.Verify(request.Password, tutor.Password)) return null;
                response = _mapper.Map<Tutor, AuthenticationResponse>(tutor);
            } 
            else
            {
                response = _mapper.Map<Student, AuthenticationResponse>(student);
            }
            response.Token = GenerateJwtToken(response.Id.ToString());
            return response;
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
                Expires = DateTime.UtcNow.AddDays(7),
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
