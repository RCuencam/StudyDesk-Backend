using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Persistence.Repositories;
using StudyDesck.API.Domain.Services;
using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Services
{
    public class PlatformService : IPlatformService
    {
        private readonly IPlatformRepository _platformRepository;
        public readonly IUnitOfWork _unitOfWork;

        public PlatformService(IPlatformRepository platformRepository, IUnitOfWork unitOfWork)
        {
            _platformRepository = platformRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PlatformResponse> DeleteAsync(int id)
        {
            var existingPlatform = await _platformRepository.FindById(id);
            if (existingPlatform == null)
                return new PlatformResponse("Platform not found");
            try
            {
                _platformRepository.Remove(existingPlatform);
                return new PlatformResponse(existingPlatform);
            }
            catch (Exception e)
            {
                return new PlatformResponse("Has ocurred an error deleting the platform " + e.Message);
            }
        }

        public async Task<PlatformResponse> GetByIdAsync(int id)
        {
            var existingPlatform = await _platformRepository.FindById(id);
            if (existingPlatform == null)
                return new PlatformResponse("Platform not found");

            return new PlatformResponse(existingPlatform);
        }

        public async Task<IEnumerable<Platform>> ListAsync()
        {
            return await _platformRepository.ListAsync();
        }

        public async Task<PlatformResponse> SaveAsync(Platform platform)
        {
            try
            {
                await _platformRepository.AddAsync(platform);
                await _unitOfWork.CompleteAsync();
                return new PlatformResponse(platform);
            }
            catch (Exception e)
            {
                return new PlatformResponse("Has ocurred an error saving the platform " + e.Message);
            }
        }

        public async Task<PlatformResponse> UpdateAsync(int id, Platform platform)
        {
            var existingPlatform = await _platformRepository.FindById(id);
            if (existingPlatform == null)
                return new PlatformResponse("platform no encontrada");

            existingPlatform.Name = platform.Name;
            try
            {
                _platformRepository.Update(existingPlatform);
                await _unitOfWork.CompleteAsync();
                return new PlatformResponse(existingPlatform);
            }
            catch (Exception e)
            {
                return new PlatformResponse("Has ocurred an error updating the platform " + e.Message);
            }
        }
    }
}
