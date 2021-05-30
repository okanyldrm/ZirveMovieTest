using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zirve.Entity.Concrete;

namespace ZirveMovie.API.Models
{
    public class EvaluationAddModel
    {
        public int Rate { get; set; }
        public int MovieId { get; set; }
        public string Note { get; set; }
    }
}
