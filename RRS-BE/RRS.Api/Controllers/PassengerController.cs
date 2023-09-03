using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RRS.Api.Data;
using RRS.Api.Models;
using RRS.Api.Repository;
using RRS.Api.ViewModels;

using IEmailSender = RRS.Api.Services.IEmailSender;

namespace RRS.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.User)]
    public class PassengerController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ITrainRepository _trainRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPassengerRepository _passengerRepository;
        SignInManager<ApplicationUser> _signInManager;
        UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        public PassengerController(ITicketRepository ticketRepository, ITrainRepository trainRepository, SignInManager<ApplicationUser> SignInManager, IUserRepository userRepository, UserManager<ApplicationUser> userManager, IPassengerRepository passengerRepository, IEmailSender emailSender)
        {
            this._ticketRepository = ticketRepository;
            this._trainRepository = trainRepository;
            this._signInManager = SignInManager;
            this._userRepository = userRepository;
            _userManager = userManager;
            this._passengerRepository = passengerRepository;
            _emailSender = emailSender;
        }

        [HttpGet("ViewPassengers")]
        public ActionResult<IEnumerable<PassengerDTO>> GetPassengers()
        {
            try
            {
                
                    var passengers =  _passengerRepository.GetPassengers(_userManager.GetUserName(User));
                    return Ok(passengers);
                
                    //return Unauthorized();
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("AddPassenger")]
        public ActionResult AddPassenger()
        {
            try
            {
                if (_signInManager.IsSignedIn(User))
                {
                    return Ok();
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("AddPassenger")]
        public ActionResult<Passenger> AddPassenger([FromBody] PassengerDTO passenger)
        {
            try
            {
                
                    //passenger.Id = _userRepository.GetUserIdAsync(_userManager.GetUserName(User)).Result;
                    
                        _passengerRepository.AddPassenger(passenger, _userManager.GetUserName(User));
                        ModelState.Clear();
                        return Ok(passenger);
                    
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
