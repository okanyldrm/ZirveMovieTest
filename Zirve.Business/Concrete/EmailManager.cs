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
    public class EmailManager : IEmailService
    {

        private readonly IEmailDal _emailDal;

        public EmailManager(IEmailDal emailDal)
        {
            _emailDal = emailDal;
        }

        public IResult EmailSave(Email entity)
        {

            try
            {
                _emailDal.EmailSave(entity);
                return new Result(success: true, message: "Email Kaydedildi");
            }
            catch (Exception e)
            {
                return new Result(success: false, message: "Email Kaydedilme Başarısız-Hata:"+e.Message);
            }
            
        }
    }
}
