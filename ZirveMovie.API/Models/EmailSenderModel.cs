using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zirve.Entity.Concrete;

namespace ZirveMovie.API.Models
{
    public class EmailSenderModel
    {
        public string Subject { get; set; }
        public string Receiver { get; set; }
        public string Body { get; set; }
        public int MovieId { get; set; }

    }
}
