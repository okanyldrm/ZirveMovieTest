using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirve.Core.Utilities.Results;
using Zirve.Entity.Concrete;
using Zirve.Entity.Dtos;

namespace Zirve.Business.Abstract
{
    public interface IMovieService
    {
        IDataResult<List<Movie>> GetAllMovie();
        IDataResult<MovieGetByIdDetailDto> GetMovieById(int id, string userId);
        IDataResult<Movie> GetMovieById(int id);
    }
}
