using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zirve.DataAccess.Abstract;
using Zirve.Entity.Concrete;

namespace Zirve.DataAccess.Concrete.Context
{
    public class NoteDal :INoteDal
    {
        public void Add(Note entity)
        {
            using (var context = new ZirveMovieContext())
            {
                var addEntity = context.Entry(entity);
                addEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }
    }
}
