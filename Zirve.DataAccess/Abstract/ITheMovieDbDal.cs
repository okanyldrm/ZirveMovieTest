using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirve.Entity.Concrete;

namespace Zirve.DataAccess.Abstract
{
    public interface ITheMovieDbDal
    {
        void AddMovieData(List<Movie> data);
    }
}
