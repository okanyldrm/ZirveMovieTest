using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zirve.Entity.Concrete;

namespace Zirve.DataAccess.Abstract
{
    public interface IEvaluationDal
    {
        Evaluation Add(Evaluation entity);
        Evaluation GetEvaluation(Expression<Func<Evaluation,bool>> filter);
        List<Evaluation> GetAllEvaluations(Expression<Func<Evaluation, bool>> filter=null);
    }
}
