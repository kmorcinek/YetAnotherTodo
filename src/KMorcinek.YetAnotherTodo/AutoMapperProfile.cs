using AutoMapper;
using KMorcinek.YetAnotherTodo.Models;

namespace KMorcinek.YetAnotherTodo
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DomainClasses.Topic, Topic>();

            CreateMap<DomainClasses.Note, Note>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(origin => origin.NoteId));
        }
    }
}