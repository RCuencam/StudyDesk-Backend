using AutoMapper;
using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services.Comunications;
using StudyDesck.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Institute, InstituteResource>();
            CreateMap<Career, CareerResource>();
            CreateMap<Student, StudentResource>();
            CreateMap<Course, CourseResource>();
            CreateMap<Topic, TopicResource>();
            CreateMap<Category, CategoryResource>();
            CreateMap<Session, SessionResource>();
            CreateMap<Platform, PlatformResource>();
            CreateMap<Tutor, TutorResource>();
            CreateMap<Schedule, ScheduleResource>();
            CreateMap<SessionReservation, SessionReservationResource>();
            CreateMap<StudyMaterial, StudyMaterialResource>();
            CreateMap<TutorReservation, TutorReservationResource>();
            CreateMap<StudentMaterial, StudentMaterialResource>();
            CreateMap<SessionMaterial, SessionMaterialResource>();
            CreateMap<Tutor, AuthenticationResponse>();
            CreateMap<Student, AuthenticationResponse>();
        }

    }
}
