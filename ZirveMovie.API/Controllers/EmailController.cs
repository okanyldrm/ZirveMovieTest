using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Zirve.Business.Abstract;
using Zirve.Core.Utilities.Results;
using Zirve.Entity.Concrete;
using ZirveMovie.API.Models;

namespace ZirveMovie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmailController : ControllerBase
    {


        private readonly IEmailService _emailService;
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public EmailController(IEmailService emailService, IMovieService movieService, IMapper mapper)
        {
            _emailService = emailService;
            _movieService = movieService;
            _mapper = mapper;
        }

        [HttpPost("send")]
        public IActionResult SendMail([FromBody]EmailSenderModel emailSenderModel)
        {
            //Sender
            var gmailSmtpUser = new GmailSmtpUser();
            gmailSmtpUser.UserName = "***";
            gmailSmtpUser.Password = "***";
            gmailSmtpUser.Port = 000;
            gmailSmtpUser.Host = "***";

            //Message
            MailMessage _mailMessage = new MailMessage();
            _mailMessage.To.Add(emailSenderModel.Receiver);
            _mailMessage.From = new MailAddress(gmailSmtpUser.UserName);
            _mailMessage.Subject =emailSenderModel.Subject;

            //Message Movie Get Movie
            var movie = _movieService.GetMovieById(emailSenderModel.MovieId);
            _mailMessage.Body = emailSenderModel.Body+"<h4>"+movie.Data.Title+"</h4>"+" OverView : "+movie.Data.Overview;
            emailSenderModel.Body = _mailMessage.Body;
            _mailMessage.IsBodyHtml = true;

            
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new NetworkCredential(gmailSmtpUser.UserName, gmailSmtpUser.Password);
                smtp.Port = gmailSmtpUser.Port;
                smtp.Host = gmailSmtpUser.Host;
                smtp.EnableSsl = true;
                smtp.Send(_mailMessage);
                var email = _mapper.Map<Email>(emailSenderModel);
                email.Sender = gmailSmtpUser.UserName;
                _emailService.EmailSave(email);
                return Ok(new Result(true,"Mail gönderildi"));
            }
            catch (Exception e)
            {
                return BadRequest(new Result(false, e.Message));
            }
          
        }




        
    }
}
