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

        // constructor
        public AuthenticationResponse(Student student, string token)
        {
            Id = student.Id;
            Email = student.Email;
            Token = token;
        }

        public AuthenticationResponse(Tutor tutor, string token)
        {
            Id = tutor.Id;
            Email = tutor.Email;
            Token = token;
        }

        public AuthenticationResponse()
        {
        }
    }
}
