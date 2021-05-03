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
    public class ExpertTopicService: IExpertTopicService
    {
        private readonly IExpertTopicRepository _expertTopicRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ExpertTopicService(IExpertTopicRepository expertTopicRepository, IUnitOfWork unitOfWork)
        {
            _expertTopicRepository = expertTopicRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ExpertTopic>> ListAsync()
        {
            return await _expertTopicRepository.ListAsync();
        }

        public async Task<IEnumerable<ExpertTopic>> ListByTopicIdAsync(int topicId)
        {
            return await _expertTopicRepository.ListByTopicIdAsync(topicId);
        }

        public async Task<IEnumerable<ExpertTopic>> ListByTutorIdAsync(int tutorId)
        {
            return await _expertTopicRepository.ListByTutorIdAsync(tutorId);
        }

        public async Task<ExpertTopicResponse> UnassignExpertTopicAsync(int tutorId, int topicId)
        {
            try
            {
                ExpertTopic expertTopic = await _expertTopicRepository.FindByTutorIdAndTopicId(tutorId, topicId);

                _expertTopicRepository.Remove(expertTopic);
                await _unitOfWork.CompleteAsync();

                return new ExpertTopicResponse(expertTopic);

            }
            catch (Exception ex)
            {
                return new ExpertTopicResponse($"An error ocurred while unassigning Topic from Tutor: {ex.Message}");
            }
        }
        public async Task<ExpertTopicResponse> AssignExpertTopicAsync(int tutorId, int topicId)
        {
            try
            {
                await _expertTopicRepository.AssignExpertTopic(tutorId, topicId);
                await _unitOfWork.CompleteAsync();
                ExpertTopic expertTopic = await _expertTopicRepository.FindByTutorIdAndTopicId(tutorId, topicId);
                return new ExpertTopicResponse(expertTopic);

            }
            catch (Exception ex)
            {
                return new ExpertTopicResponse($"An error ocurred while assigning Topic from Tutor: {ex.Message}");
            }
        }
    }
}
