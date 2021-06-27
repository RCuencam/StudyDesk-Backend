using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services
{
    public interface IUserService
    {
        // Nuestro Service, tendra un metodo adicional "Authenticate"
        AuthenticationResponse Authenticate(AuthenticationRequest request);
        //IEnumerable<User> GetAll();
    }
}
