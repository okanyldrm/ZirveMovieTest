using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zirve.Business.Abstract;
using Zirve.Core.Utilities.Results;
using Zirve.DataAccess.Abstract;
using Zirve.Entity.Concrete;

namespace Zirve.Business.Concrete
{
    public class EvaluationManager : IEvaluationService
    {
        private readonly IEvaluationDal _evaluationDal;



        public EvaluationManager(IEvaluationDal evaluationDal)
        {
            _evaluationDal = evaluationDal;
        }


        public IDataResult<Evaluation> Add(Evaluation entity)
        {
            try
            {
                var hasEvalation = _evaluationDal.GetAllEvaluations(e => e.ApplicationUserId == entity.ApplicationUserId && e.MovieId == entity.MovieId).Count;
                if (hasEvalation == 0)
                {
                    if (entity.Rate >= 0 && entity.Rate <= 10)
                    {
                        return new DataResult<Evaluation>(data: _evaluationDal.Add(entity),success:true,message:"Ekleme Başarılı") ;
                    }
                    else
                    {
                        var exp = new Exception("Değerlendirme puanı 1-10 arasında tam değer almalıdır").Message;
                        return new DataResult<Evaluation>(data:null,success:false,message:exp);

                    }

                }
                else
                {
                    var exp = new Exception("Kullanıcı bu film hakkında değerlendirme yapmıştır").Message;
                    return new DataResult<Evaluation>(data: null, success: false, message: exp);
                }

            }
            catch (Exception e)
            {
                return new DataResult<Evaluation>(data: null, success: false,e.Message);
            }
            
        }

        public IDataResult<List<Evaluation>> GetAllEvaluation(Expression<Func<Evaluation, bool>> filter)
        {
            try
            {
                var data = _evaluationDal.GetAllEvaluations();
                return new DataResult<List<Evaluation>>(data: data, success: true, message: "Listeleme Başarılı");
            }
            catch (Exception e)
            {
                return new DataResult<List<Evaluation>>(data: null, success: false, message: e.Message);
            }
        }
    }
}
