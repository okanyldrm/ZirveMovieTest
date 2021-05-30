using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zirve.DataAccess.Abstract;
using Zirve.Entity.Concrete;
using Zirve.Entity.Dtos;

namespace Zirve.DataAccess.Concrete
{
    public class MovieDal : IMovieDal
    {
        private readonly IMapper _mapper;
        public MovieDal(IMapper mapper)
        {
            _mapper = mapper;
        }


        public List<Movie> GetAllMovie()
        {
            using (var context = new ZirveMovieContext())
            {
                return context.Movies.ToList();
            }
        }

        public MovieGetByIdDetailDto GetMovie(int id,string userId)
        {

            using (var context = new ZirveMovieContext())
            {

                //query
                var movie = context.Movies
                        .Include(m => m.Evaluations)
                        .ThenInclude(e => e.Notes)
                        .SingleOrDefault(m => m.Id == id);
                var userEvelatuion = movie.Evaluations.Any(p => p.ApplicationUserId == userId && movie.Id==id);

                if (movie!=null&& userEvelatuion)
                {
                    var rate = movie.Evaluations.SingleOrDefault(k => k.ApplicationUserId.Equals(userId)).Rate;
                    var notes = movie.Evaluations.SingleOrDefault(k => k.ApplicationUserId.Equals(userId)).Notes;
                    var avgRate = movie.Evaluations.Average(p => p.Rate);

                    //mapping
                    var movieDto = _mapper.Map<MovieDto>(movie);
                    var notesDto = _mapper.Map<List<NoteDto>>(notes);

                    //result
                    var result = new MovieGetByIdDetailDto();
                    result.Movie = movieDto;
                    result.Rate = rate;
                    result.Notes = notesDto;
                    result.AvgRate = avgRate;

                    return result;
                }
                else if (movie != null && !userEvelatuion)
                {
                    
                    
                    //mapping
                    var movieDto = _mapper.Map<MovieDto>(movie);
                    //result
                    var result = new MovieGetByIdDetailDto();
                    result.Movie = movieDto;
                    result.Rate = -1;
                    return result;
                }
                else
                {
                    throw new Exception("Film Bulunamadı");
                }

               
            }
            
        }

        public Movie GetMovie(int id)
        {
            using (var context = new ZirveMovieContext())
            {
                var result = context.Movies.SingleOrDefault(m => m.Id == id);
                return result;
            }
        }
    }
}
