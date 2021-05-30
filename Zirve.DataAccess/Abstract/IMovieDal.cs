using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirve.Entity.Concrete;
using Zirve.Entity.Dtos;

namespace Zirve.DataAccess.Abstract
{
    public interface IMovieDal
    {
        List<Movie> GetAllMovie();
        MovieGetByIdDetailDto GetMovie(int id,string userId);
        Movie GetMovie(int id);
    }
}
