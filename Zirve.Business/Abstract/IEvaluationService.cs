using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zirve.Core.Utilities.Results;
using Zirve.Entity.Concrete;

namespace Zirve.Business.Abstract
{
    public interface IEvaluationService
    {
        IDataResult<Evaluation> Add(Evaluation entity);
        IDataResult<List<Evaluation>> GetAllEvaluation(Expression<Func<Evaluation, bool>> filter);

    }
}
