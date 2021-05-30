using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zirve.Business.Abstract;

namespace ZirveMovie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheMovieDbServiceController : ControllerBase
    {

        private readonly ITheMovieDbService _getMoviesUseService;

        public TheMovieDbServiceController(ITheMovieDbService getMoviesUseService)
        {
            _getMoviesUseService = getMoviesUseService;
        }
        public TheMovieDbServiceController(){}


        [HttpGet(template: "getmovies")]
        public async Task<IActionResult> GetMovie()
        {
            _getMoviesUseService.GetMovies();

            //hangfire test
            //Console.WriteLine("Liste Çekildi");
            return Ok();
        }
    }
}
