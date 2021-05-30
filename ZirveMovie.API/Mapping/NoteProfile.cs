using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zirve.Entity.Concrete;
using Zirve.Entity.Dtos;

namespace ZirveMovie.API.Mapping
{
    public class NoteProfile : Profile
    {
        public NoteProfile()
        {
            CreateMap<Note, NoteDto>();
        }
    }
}
