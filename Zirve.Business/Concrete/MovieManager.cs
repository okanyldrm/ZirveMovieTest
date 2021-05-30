using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirve.Business.Abstract;
using Zirve.Core.Utilities.Messages;
using Zirve.Core.Utilities.Results;
using Zirve.DataAccess.Abstract;
using Zirve.Entity.Concrete;
using Zirve.Entity.Dtos;

namespace Zirve.Business.Concrete
{
    public class MovieManager : IMovieService
    {

        private readonly IMovieDal _movieDal;

        public MovieManager(IMovieDal movieDal)
        {
            _movieDal = movieDal;
        }

      
        public IDataResult<List<Movie>> GetAllMovie()
        {
            //todo sayfa sayısı fazla girilince yinede true dönüyor onu kontrol et
            try
            {
                return new DataResult<List<Movie>>(data: _movieDal.GetAllMovie(),success:true,message:MovieMessage.MovieListSuccess) ;
            }
            catch (Exception e)
            {
                return new DataResult<List<Movie>>(data: null, success: false, message:e.Message);
            }
        }

        public IDataResult<MovieGetByIdDetailDto> GetMovieById(int id, string userId)
        {
            try
            {
                return  new DataResult<MovieGetByIdDetailDto>(data: _movieDal.GetMovie(id, userId),success:true,message:"Film Getirme Başarılı") ;
            }
            catch (Exception e)
            {
                return new DataResult<MovieGetByIdDetailDto>(data: null, success: false, message: e.Message);
            }
        }

        public IDataResult<Movie> GetMovieById(int id)
        {
            try
            {
                return new DataResult<Movie>(data: _movieDal.GetMovie(id), success: true, message: "Film Getirme Başarılı");
            }
            catch (Exception e)
            {
                return new DataResult<Movie>(data: null, success: false, message: e.Message);
            }
        }
    }
}
