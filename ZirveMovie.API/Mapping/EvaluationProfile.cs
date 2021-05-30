using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zirve.Entity.Concrete;
using ZirveMovie.API.Models;

namespace ZirveMovie.API.Mapping
{
    public class EvaluationProfile:Profile
    {


        public EvaluationProfile()
        {
            CreateMap<EvaluationAddModel, Evaluation>();
        }

    }
}
