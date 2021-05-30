using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zirve.DataAccess.Abstract;
using Zirve.Entity.Concrete;

namespace Zirve.DataAccess.Concrete
{
    public class TheMovieDbDal : ITheMovieDbDal
    {
        public void AddMovieData(List<Movie> data)
        {
            using (var _context = new ZirveMovieContext())
            {
                foreach (var movie in data)
                {
                    var addedEntity = _context.Entry(movie);
                    addedEntity.State = EntityState.Added;
                    _context.SaveChanges();
                }
            }
        }
    }
}
