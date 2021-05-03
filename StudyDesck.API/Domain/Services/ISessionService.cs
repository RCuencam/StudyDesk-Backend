using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services
{
    public interface ISessionService
    {
        Task<IEnumerable<Session>> ListAsync();
        Task<SessionResponse> GetByIdAsync(int id);
        Task<SessionResponse> SaveAsync(Session session);
        Task<SessionResponse> UpdateAsync(int id, Session session);
        Task<SessionResponse> DeleteAsync(int id);
    }
}
