using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zirve.DataAccess.Abstract;
using Zirve.Entity.Concrete;

namespace Zirve.DataAccess.Concrete
{
    public class EvaluationDal : IEvaluationDal
    {
        public Evaluation Add(Evaluation entity)
        {

            using (var context = new ZirveMovieContext())
            {
                var addEntity = context.Entry(entity);
                    addEntity.State = EntityState.Added;
                    context.SaveChanges();
                    return GetEvaluation(m => m.Id == entity.Id);
            }

        }

        public Evaluation GetEvaluation(Expression<Func<Evaluation, bool>> filter)
        {
            using (var context = new ZirveMovieContext())
            {
                return context.Set<Evaluation>().SingleOrDefault(filter);
            }

        }

        public List<Evaluation> GetAllEvaluations(Expression<Func<Evaluation, bool>> filter)
        {
            using (var context = new ZirveMovieContext())
            {
                return filter == null ? context.Set<Evaluation>().ToList() : context.Set<Evaluation>().Where(filter).ToList();
            }
           
        }
    }
}
