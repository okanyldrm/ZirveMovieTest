using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirve.Entity.Concrete;

namespace Zirve.Entity.Dtos
{
    public class MovieGetByIdDetailDto
    {
        public MovieDto Movie { get; set; }
        public int Rate { get; set; }
        public List<NoteDto> Notes { get; set; }
        public double AvgRate { get; set; }
    }
}
