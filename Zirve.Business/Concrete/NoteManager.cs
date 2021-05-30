using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirve.Business.Abstract;
using Zirve.Core.Utilities.Results;
using Zirve.DataAccess.Abstract;
using Zirve.Entity.Concrete;

namespace Zirve.Business.Concrete
{
    public class NoteManager : INoteService
    {

        private readonly INoteDal _noteDal;
        private readonly IEvaluationDal _evaluationDal;

        public NoteManager(INoteDal noteDal, IEvaluationDal evaluationDal)
        {
            _noteDal = noteDal;
            _evaluationDal = evaluationDal;
        }


        public IResult Add(Note entity)
        {
            var hasEvaluation = _evaluationDal.GetEvaluation(e => e.Id == entity.EvaluationId);
            if (hasEvaluation!=null)
            {
                try
                {
                    _noteDal.Add(entity);
                    return new Result(true, "Not Ekleme Başarılı");
                }
                catch (Exception e)
                {
                    return new Result(false, e.Message);
                }
            }
            else
            {
                return new Result(false, "Böyle bir değerlendirme mevcut değil önce değerlendirme yapınız");
            }

          
          
        }
    }
}
