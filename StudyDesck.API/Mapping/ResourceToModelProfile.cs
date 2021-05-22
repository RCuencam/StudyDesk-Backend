using AutoMapper;
using StudyDesck.API.Domain.Models;
using StudyDesck.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveInstituteResource, Institute>();
            CreateMap<SaveCareerResource, Career>();
            CreateMap<SaveStudentResource, Student>();
            CreateMap<SaveCourseResource, Course>();
            CreateMap<SaveTopicResource, Topic>();
            CreateMap<SaveCategoryResource, Category>();
            CreateMap<SavePlatformResource, Platform>();
            CreateMap<SaveSessionResource, Session>();
            CreateMap<SaveTutorResource, Tutor>();
            CreateMap<SaveSheduleResource, Shedule>();
            CreateMap<SaveSessionReservationResource, SessionReservation>();
        }
    }
}
