using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using Zirve.Business.Abstract;
using Zirve.Core.Utilities.Results;
using Zirve.Entity.Concrete;

namespace ZirveMovie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }


        [HttpGet(template: "getmovies/{pageNumber}/{pageSize}")]
        public IActionResult GetMovie(int pageNumber, int pageSize)
        {
            
            var movies = _movieService.GetAllMovie();
           
           
                if (movies.Success)
                {
                   
                        var maxPageNumber = Math.Ceiling(((double)movies.Data.Count / pageSize));
                        if (maxPageNumber>=pageNumber)
                        {
                             var data = movies.Data.Skip(count: (pageNumber - 1) * pageSize).Take(count: pageSize).ToList();
                             movies.Data = data;
                             return Ok(movies);
                        }
                        
                    

                    return BadRequest(new Result(false, "istenilen sayfa sayısı ve boyutu toplam film sayısını aştı"));

                }
                else
                {
                    return BadRequest(new Result(false,movies.Message));
                }
           
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetMovieById(int id)
        {
           
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = _movieService.GetMovieById(id,userId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new Result(false, result.Message));
            }
        }

    }
}
