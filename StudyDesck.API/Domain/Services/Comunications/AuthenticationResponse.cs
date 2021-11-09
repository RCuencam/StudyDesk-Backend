using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services.Comunications
{
    public class AuthenticationResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public bool IsTutor { get; set; }
        public string Message { get; set; }

        // constructor
        public AuthenticationResponse(Student student, string token, bool isTutor)
        {
            Id = student.Id;
            Email = student.Email;
            IsTutor = isTutor;
            Token = token;
        }

        public AuthenticationResponse(Tutor tutor, string token, bool isTutor)
        {
            Id = tutor.Id;
            Email = tutor.Email;
            IsTutor = isTutor;
            Token = token;
        }

        public AuthenticationResponse(string message)
        {
            Message = message;
        }

        public AuthenticationResponse() { }
       
    }
}
