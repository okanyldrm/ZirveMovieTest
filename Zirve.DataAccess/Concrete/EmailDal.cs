using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration.Annotations;
using Microsoft.EntityFrameworkCore;
using Zirve.DataAccess.Abstract;
using Zirve.Entity.Concrete;

namespace Zirve.DataAccess.Concrete
{
    public class EmailDal : IEmailDal
    {
        public void EmailSave(Email entity)
        {
            using (var context= new ZirveMovieContext())
            {
                var addedEntiy = context.Entry(entity);
                addedEntiy.State = EntityState.Added;
                context.SaveChanges();
            }
        }
    }
}
