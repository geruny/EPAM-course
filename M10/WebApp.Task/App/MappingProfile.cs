using System.Collections.Generic;
using App.AppModels;
using App.AppPostModels;
using App.Domain.core.Models;
using AutoMapper;

namespace App
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StudentAppPost, Student>();
            CreateMap<LectorAppPost, Lector>();
            CreateMap<LectureAppPost, Lecture>();
            CreateMap<HomeworkAppPost, Homework>();

            CreateMap<Student, StudentApp>();
            CreateMap<Lecture, LectureApp>();
            CreateMap<Homework, HomeworkApp>();

            CreateMap<StudentApp, Student>();
            CreateMap<LectureApp, Lecture>();
            CreateMap<HomeworkApp, Homework>();
        }
    }
}