using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
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
    public class EvaluationController : ControllerBase
    {

        private readonly IEvaluationService _evaluationService;
        private readonly INoteService _noteService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper _mapper;

        public EvaluationController(IEvaluationService evaluationService, UserManager<ApplicationUser> userManager, INoteService noteService, IMapper mapper)
        {
            _evaluationService = evaluationService;
            this.userManager = userManager;
            _noteService = noteService;
            _mapper = mapper;
        }


        [HttpPost("add")]
        public async Task<IActionResult> AddEvaluation([FromBody]EvaluationAddModel entity)
        {
            //var currentUser = await userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            

            var addmodel = _mapper.Map<Evaluation>(entity);
            addmodel.ApplicationUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var evaluation = _evaluationService.Add(addmodel);

            if (evaluation.Success)
            {
                var note = new Note();
                note.EvaluationId = evaluation.Data.Id;
                note.NoteText = entity.Note;
                _noteService.Add(note);
                var result = new Result(true, evaluation.Message);
                return Ok(result);
            }
            else
            {
                var result = new Result(false, evaluation.Message);
                return Ok(result);
            }
        }




    }
}
