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
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IUnitOfWork _unitOfWork;
        public TopicService(ITopicRepository repository, IUnitOfWork unitOfWork)
        {
            _topicRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TopicResponse> DeleteAsync(int id)
        {
            var existingTopic = await _topicRepository.FindById(id);
            if (existingTopic == null)
                return new TopicResponse("Topic not found");
            try
            {
                _topicRepository.Remove(existingTopic);
                await _unitOfWork.CompleteAsync();
                return new TopicResponse(existingTopic);
            }
            catch (Exception e)
            {
                return new TopicResponse("Has ocurred an error deleting the Topic" + e.Message);
            }
        }

        public async Task<TopicResponse> GetByIdAsync(int id)
        {
            var existingTopic = await _topicRepository.FindById(id);
            if (existingTopic == null)
                return new TopicResponse("Topic not found");
            return new TopicResponse(existingTopic);

        }

        public async Task<IEnumerable<Topic>> ListAsync()
        {
            return await _topicRepository.ListAsync();
        }

        public async Task<TopicResponse> SaveAsync(Topic Topic)
        {
            try
            {
                await _topicRepository.AddAsync(Topic);
                await _unitOfWork.CompleteAsync();
                return new TopicResponse(Topic);
            }
            catch (Exception e)
            {
                return new TopicResponse("Has ocurred an error saving the Topic" + e.Message);
            }

        }

        public async Task<TopicResponse> UpdateAsync(int id, Topic Topic)
        {
            var existingTopic = await _topicRepository.FindById(id);
            if (existingTopic == null)
                return new TopicResponse("Topic not found");

            existingTopic.Name = Topic.Name;
            try
            {
                _topicRepository.Update(existingTopic);
                await _unitOfWork.CompleteAsync();
                return new TopicResponse(existingTopic);
            }
            catch (Exception e)
            {
                return new TopicResponse("Has ocurred an error updating the Topic" + e.Message);
            }
        }
    }
}
