using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Zirve.Business.Abstract;
using Zirve.Core.Utilities.Results;
using Zirve.Entity.Concrete;
using ZirveMovie.API.Models;

namespace ZirveMovie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteController : ControllerBase
    {

        private readonly INoteService _noteService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEvaluationService _evaluationService;

        public NoteController(INoteService noteService, UserManager<ApplicationUser> userManager, IEvaluationService evaluationService)
        {
            _noteService = noteService;
            this.userManager = userManager;
            _evaluationService = evaluationService;
        }

        //todo test et add yapmadın var olan bir evelautiona not ekleme işlemi
        [HttpPost("add")]
        public IActionResult Add([FromBody]NoteAddModel entity)
        {
            
                var addModel = new Note();
                addModel.EvaluationId = entity.EvaluationId;
                addModel.NoteText = entity.NoteText;
                var result = _noteService.Add(addModel);
                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
              
            
        }


    }
}
