using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Zirve.Entity.Concrete
{
    public class ApplicationUser : IdentityUser
    {
        public List<Evaluation> Evaluations { get; set; }
    }
}
