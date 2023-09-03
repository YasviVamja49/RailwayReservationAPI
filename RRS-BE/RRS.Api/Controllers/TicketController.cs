using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RRS.Api.Data;
using RRS.Api.Models;
using RRS.Api.Repository;
using RRS.Api.Services;
using RRS.Api.ViewModels;
using System.Security.Claims;

namespace RRS.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.User)]
    public class TicketController : ControllerBase
    {
        ITicketRepository _ticketRepository;
        ITrainRepository _trainRepository;
        IUserRepository _userRepository;
        SignInManager<ApplicationUser> _signInManager;
        UserManager<ApplicationUser> _userManager;
        Random random = new Random();
        IEmailSender _emailSender;

        public TicketController(ITicketRepository ticketRepository, ITrainRepository trainRepository, SignInManager<ApplicationUser> signInManager, IUserRepository userRepository, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _ticketRepository = ticketRepository;
            _trainRepository = trainRepository;
            _signInManager = signInManager;
            _userRepository = userRepository;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        
        public ActionResult<IEnumerable<TicketDTOoutput>> ViewTicket()
        {
            try
            {
                
                    List<TicketDTOoutput> ticket = _ticketRepository.GetTicketInfo(_userManager.GetUserName(User));
                    return Ok(ticket);
                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving tickets from the database");
            }
        }


        [HttpPost]
        
        public ActionResult<TicketDTOinput> BookTicket(TicketDTOinput ticketinput)
        {
            try
            {
                TrainDTO t = _trainRepository.GetTrainInfo(ticketinput.TrainNo);
                if (ticketinput != null)
                {


                    string status;
                    int b, c;
                    if (_trainRepository.GetSeatCount(t.TrainName, ticketinput.Ticketclass.ToLower()) > 0)
                    {
                        status = "Confirmed";
                        b = random.Next(1, _trainRepository.GetSeatCount(t.TrainName, ticketinput.Ticketclass.ToLower()) + 1);
                        c = random.Next(1, 10);
                        _trainRepository.UpdateSeatCount(t.TrainName, ticketinput.Ticketclass.ToLower());
                    }
                    else
                    {
                        status = "Waiting";
                        b = 0;
                        c = 0;
                    }


                    TicketDTOoutput ticket = new TicketDTOoutput()
                    {
                        //Ticketno = ticketinput.,
                        PassengerName = ticketinput.PassengerName,
                        Ticketclass = ticketinput.Ticketclass,
                        TrainNo = ticketinput.TrainNo,
                        Bookingstatus = status,
                        Bookingdate = DateTime.Now.Date.ToString("yyyy/MM/dd"),//change
                        Arrivaldate = t.Arrivaldate,
                        Berthno = b,
                        Coachno = c
                    };



                    
                    string user = _userManager.GetUserName(User);
                    _ticketRepository.TicketCreation(ticket,user);
                    //var currentUser = await _userManager.GetUserAsync(User);
                    //string email = _userManager.GetEmailAsync(User).Result;
                    bool isSuccess = SendEmailAsync(user, ticket).Result;
                    if (isSuccess){
                        return ticket;
                    }
                    else
                    {
                        return StatusCode(501, new { Message = "Ticket Generated But Email Notification Failed" });
                    }
                    
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpDelete("{ticketNo}")]
        public ActionResult<TicketDTOinput> CancelTicket(int ticketNo)
        {
            try
            {
                // Get the username of the authenticated user
                string username = _userManager.GetUserName(User);
                TicketDTOoutput ticket=_ticketRepository.GetTicketByTicketNo(ticketNo,username);
                _ticketRepository.TicketCancel(ticketNo);
                _trainRepository.UpdateSeatCountOnCancel(ticket.TrainNo, ticket.Ticketclass);
                string user = _userManager.GetUserName(User);
                bool isSuccess = SendCancelEmailAsync(user, ticket).Result;
                if (isSuccess)
                {
                    return Ok("Ticket Cancelled");
                }
                else
                {
                    return StatusCode(501, new { Message = "Ticket Canceled But Email Notification Failed" });
                }
                
                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error canceling the ticket.");
            }
        }


        private async Task<bool> SendEmailAsync(string recipientEmail,TicketDTOoutput ticket)
        {
            try
            {
                string messageStatus = await _emailSender.SendEmailAsync(recipientEmail,ticket);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private async Task<bool> SendCancelEmailAsync(string recipientEmail, TicketDTOoutput ticket)
        {
            try
            {
                string messageStatus = await _emailSender.SendCancellationEmailAsync(recipientEmail, ticket);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }

    

}

