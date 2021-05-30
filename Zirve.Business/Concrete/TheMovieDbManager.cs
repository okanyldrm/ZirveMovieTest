using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Zirve.Business.Abstract;
using Zirve.DataAccess.Abstract;
using Zirve.Entity.Concrete;

namespace Zirve.Business.Concrete
{
    public class TheMovieDbManager : ITheMovieDbService
    {


        private readonly ITheMovieDbDal _movieServiceDal;
        private readonly IMovieService _movieService;

        public TheMovieDbManager(ITheMovieDbDal movieServiceDal, IMovieService movieService)
        {
            _movieServiceDal = movieServiceDal;
            _movieService = movieService;
        }


     

        public async void GetMovies()
        {
            const int sizeData = 24;
            var httpClient = HttpClientFactory.Create();
            Movie movie = new Movie();
            List<Movie> movies = new List<Movie>();
            string api_key = "59d3671137d4181a7acc8a0061bddebe";


            try
            {
                for (int i = 3; i < sizeData; i++)
                {
                    //var url = "https://api.themoviedb.org/3/movie/" + i + "?api_key=59d3671137d4181a7acc8a0061bddebe";
                    var url = "https://api.themoviedb.org/3/movie/" + i + "?api_key=" + api_key;
                    var response = await httpClient.GetAsync(url);
                    var statusCode = response.StatusCode.ToString();
                    if (statusCode == "OK")
                    {
                        var data = await httpClient.GetStringAsync(url);
                        movie = Newtonsoft.Json.JsonConvert.DeserializeObject<Movie>(data);
                        var moviesDb = _movieService.GetAllMovie();
                        if (!moviesDb.Data.Any(m => m.Id == movie.Id))
                        {
                            movies.Add(movie);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            _movieServiceDal.AddMovieData(movies);
        }
    }
}
