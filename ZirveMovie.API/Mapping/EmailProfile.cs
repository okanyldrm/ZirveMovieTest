using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Zirve.Entity.Concrete;
using ZirveMovie.API.Models;

namespace ZirveMovie.API.Mapping
{
    public class EmailProfile : Profile
    {

        public EmailProfile()
        {
            CreateMap<EmailSenderModel,Email>();
        }

    }
}
