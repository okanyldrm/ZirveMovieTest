using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Zirve.Entity.Concrete;
using Zirve.Entity.Dtos;

namespace ZirveMovie.API.Mapping
{
    public class MovieProfile :Profile
    {

        public MovieProfile()
        {
            CreateMap<Movie,MovieDto>();
        }
    }
}
