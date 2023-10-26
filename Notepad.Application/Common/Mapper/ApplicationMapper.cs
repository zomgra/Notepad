using AutoMapper;
using Notepad.Domain.Entities;
using Notepad.Domain.ViewModel;

namespace Notepad.Application.Common.Mapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Note, NoteViewModel>();
            CreateMap<NoteChunk, ChunkViewModel>();
            CreateMap<User, UserViewModel>();

        }
    }
}
