using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using RRS.Api.Data;

using RRS.Api.Repository;
using RRS.Api.ViewModels;

namespace RRS.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles =UserRoles.Admin)] 
    public class TrainController : ControllerBase
    {
        ITicketRepository ticketRepository;
        ITrainRepository trainRepository;
        SignInManager<ApplicationUser> SignInManager;

        public TrainController(ITicketRepository ticketRepository, ITrainRepository trainRepository, SignInManager<ApplicationUser> SignInManager)
        {
            this.ticketRepository = ticketRepository;
            this.trainRepository = trainRepository;
            this.SignInManager = SignInManager;
        }

        [HttpGet]
        [Route("GetAll")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<TrainDTO>> GetAllTrains()
        {
            try
            {
               
                        List<TrainDTO> trains = trainRepository.GetAllTrains();
                        
                        return Ok(trains);
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("AddTrain")]
        public ActionResult<TrainDTO> AddTrain([FromBody] TrainDTO trainDto)
        {
            try
            {

                    trainRepository.AddTrain(trainDto);
                    return Ok(trainDto);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost]
        //public IActionResult AddTrain([FromBody] Train train)
        //{
        //    try
        //    {
        //        if (User.IsInRole("Admin"))
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                trainRepository.AddTrain(train);
        //                return Ok();
        //            }
        //            else
        //            {
        //                return BadRequest(ModelState);
        //            }
        //        }
        //        else
        //        {
        //            return Unauthorized();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpGet]
        [Route("GetTrainById/{trainid}")]
        [AllowAnonymous]
        public ActionResult<TrainDTO> GetTrain(int trainid)
        {
            try
            {
                
                    var trainDTO = trainRepository.GetTrainInfo(trainid);

                    if (trainDTO == null)
                        return NotFound();

                    

                    return Ok(trainDTO);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("EditTrainById/{trainid}")]
        public ActionResult<TrainDTO> EditTrain(int trainid, [FromBody] TrainDTO trainDTO)
        {
            try
            {
                var existingTrain = trainRepository.GetTrainInfo(trainid);

                    if (existingTrain == null)
                        return NotFound();

                existingTrain.TrainName = trainDTO.TrainName;
                existingTrain.Startloc = trainDTO.Startloc;
                existingTrain.Endloc = trainDTO.Endloc;
                existingTrain.Arrivaltime = trainDTO.Arrivaltime;
                existingTrain.Departuretime = trainDTO.Departuretime;
                existingTrain.Arrivaldate = trainDTO.Arrivaldate;
                existingTrain.Ac1tier = trainDTO.Ac1tier;
                existingTrain.Ac2tier = trainDTO.Ac2tier;
                existingTrain.Ac3tier = trainDTO.Ac3tier;
                existingTrain.Sleeper = trainDTO.Sleeper;
                existingTrain.Tatkal = trainDTO.Tatkal;
                existingTrain.Ladies = trainDTO.Ladies;
                existingTrain.BaseFare = trainDTO.BaseFare;

                trainRepository.EditTrain(existingTrain);

                    return Ok(trainDTO);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteTrainById/{trainid}")]
        public ActionResult<TrainDTO> DeleteTrain(int trainid)
        {
            try
            {
               
                    var existingTrain = trainRepository.GetTrainInfo(trainid);

                    if (existingTrain == null)
                        return NotFound();

                    trainRepository.DeleteTrain(existingTrain);

                    return Ok(new { message = "Train deleted successfully." });
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        //[HttpGet("{trainid}")]
        //public ActionResult<TrainDTO> GetTrainDetails(int trainid)
        //{
        //    try
        //    {
                
        //            var train = trainRepository.GetTrainInfo(trainid);

        //            if (train == null)
        //                return NotFound();

        //            return Ok(train);
               
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpGet]
        [Route("Search")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<TrainDTO>> SearchTrains([FromQuery] string startloc, [FromQuery] string endloc, [FromQuery] string date)
        {
            try
            {
                var trainslist = trainRepository.GetAllTrains();

                if (!string.IsNullOrEmpty(startloc) && !string.IsNullOrEmpty(endloc) && !string.IsNullOrEmpty(date))
                {
                    var trains = trainslist.Where(t => t.Startloc.Contains(startloc) && t.Endloc.Contains(endloc) && t.Arrivaldate.Contains(date));
                    return Ok(trains);
                }
                else
                {
                    return Ok(trainslist);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[HttpGet("{trainid}")]
        //public IActionResult BookTicket(int trainid)
        //{
        //    try
        //    {
        //        if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
        //        {
        //            Train train = _trainRepository.GetTrainInfo(trainid);
        //            HttpContext.Session.SetString("trainname", train.TrainName);
        //            HttpContext.Session.SetString("startloc", train.Startloc);
        //            HttpContext.Session.SetString("endloc", train.Endloc);
        //            HttpContext.Session.SetString("arrivaltime", train.Arrivaltime);
        //            HttpContext.Session.SetString("departuretime", train.Departuretime);
        //            HttpContext.Session.SetString("arrivaldate", train.Arrivaldate);
        //            HttpContext.Session.SetInt32("Ac1tier", train.Ac1tier);
        //            HttpContext.Session.SetInt32("Ac2tier", train.Ac2tier);
        //            HttpContext.Session.SetInt32("Ac3tier", train.Ac3tier);
        //            HttpContext.Session.SetInt32("Sleeper", train.Sleeper);
        //            HttpContext.Session.SetInt32("Tatkal", train.Tatkal);
        //            HttpContext.Session.SetInt32("Ladies", train.Ladies);

        //            List<SelectListItem> lst = new List<SelectListItem>();
        //            lst.Add(new SelectListItem { Text = "AC-1 Tier", Value = "Ac1tier" });
        //            lst.Add(new SelectListItem { Text = "AC-2 Tier", Value = "Ac2tier" });
        //            lst.Add(new SelectListItem { Text = "AC-3 Tier", Value = "Ac3tier" });
        //            lst.Add(new SelectListItem { Text = "Sleeper", Value = "Sleeper" });
        //            lst.Add(new SelectListItem { Text = "Tatkal", Value = "Tatkal" });
        //            lst.Add(new SelectListItem { Text = "Ladies", Value = "Ladies" });
        //            ViewBag.TrainClasses = lst;

        //            List<SelectListItem> Gender = new List<SelectListItem>();
        //            Gender.Add(new SelectListItem { Text = "Male", Value = "Male" });
        //            Gender.Add(new SelectListItem { Text = "Female", Value = "Female" });
        //            ViewBag.Genders = Gender;

        //            List<Passenger> pList = _ticketRepository.GetPassengers((int)HttpContext.Session.GetInt32("UserId"));
        //            List<SelectListItem> PassengerName = new List<SelectListItem>();

        //            foreach (var p in pList)
        //            {
        //                PassengerName.Add(new SelectListItem
        //                { Text = p.PassengerName, Value = p.PassengerId.ToString() });
        //            }
        //            ViewBag.Passenger = PassengerName;

        //            return Ok();
        //        }
        //        else
        //        {
        //            return Unauthorized();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult BookTicket(Ticket ticket, FormCollection form)
        //{
        //    Train t = trainRepository.GetTrainByName(HttpContext.Session["trainname"].ToString());
        //    if (ticket != null)
        //    {
        //        Ticket ticket1 = new Ticket();
        //        //Passenger p=ticketRepository.GetPassengerById(ticket.PassengerId);
        //        ticket1.Bookingstatus = null;
        //        ticket1.TrainNo = t.TrainNo;
        //        ticket1.Ticketclass = form["TrainClasses"].ToString();
        //        ticket1.PassengerId = ticket.PassengerId;
        //        ticket1.Berthno = ticket.Berthno;
        //        ticket1.Coachno = ticket.Coachno;
        //        ticket1.Arrivaldate = HttpContext.Session["arrivaldate"].ToString();
        //        ticket1.Bookingdate = DateTime.Now.Date.ToString("dd/MM/yyyy");//change
        //        ticket1.PassengerId = Convert.ToInt32(form["Passenger"]);

        //        if (ticket1.Ticketclass == "Ac2tier")
        //        {
        //            if (trainRepository.GetAc2tierCount(t.TrainName) > 0)
        //            {
        //                ticket1.Bookingstatus = "Confirmed";
        //                ticketRepository.TicketCreation(ticket1);
        //                trainRepository.UpdateAc2tierCount(t.TrainName);
        //            }
        //            else
        //            {
        //                ticket1.Bookingstatus = "Waitlist";
        //                ticketRepository.TicketCreation(ticket1);
        //                trainRepository.UpdateAc2tierCount(t.TrainName);
        //            }

        //        }
        //        else if (ticket1.Ticketclass == "Ac3tier")
        //        {
        //            if (trainRepository.GetAc3tierCount(t.TrainName) > 0)
        //            {
        //                ticket1.Bookingstatus = "Confirmed";
        //                ticketRepository.TicketCreation(ticket1);
        //                trainRepository.UpdateAc3tierCount(t.TrainName);
        //            }
        //            else
        //            {
        //                ticket1.Bookingstatus = "Waitlist";
        //                ticketRepository.TicketCreation(ticket1);
        //                trainRepository.UpdateAc3tierCount(t.TrainName);
        //            }
        //        }
        //        else if (ticket1.Ticketclass == "Sleeper")
        //        {
        //            if (trainRepository.GetSleeperCount(t.TrainName) > 0)
        //            {
        //                ticket1.Bookingstatus = "Confirmed";
        //                ticketRepository.TicketCreation(ticket1);
        //                trainRepository.UpdateSleeperCount(t.TrainName);
        //            }
        //            else
        //            {
        //                ticket1.Bookingstatus = "Waitlist";
        //                ticketRepository.TicketCreation(ticket1);
        //                trainRepository.UpdateSleeperCount(t.TrainName);
        //            }
        //        }
        //        else if (ticket1.Ticketclass == "Tatkal")
        //        {
        //            if (trainRepository.GetTatkalCount(t.TrainName) > 0)
        //            {
        //                ticket1.Bookingstatus = "Confirmed";
        //                ticketRepository.TicketCreation(ticket1);
        //                trainRepository.UpdateTatkalCount(t.TrainName);
        //            }
        //            else
        //            {
        //                ticket1.Bookingstatus = "Waitlist";
        //                ticketRepository.TicketCreation(ticket1);
        //                trainRepository.UpdateTatkalCount(t.TrainName);
        //            }
        //        }
        //        else
        //        {
        //            if (trainRepository.GetLadiesCount(t.TrainName) > 0)
        //            {
        //                ticket1.Bookingstatus = "Confirmed";
        //                ticketRepository.TicketCreation(ticket1);
        //                trainRepository.UpdateLadiesCount(t.TrainName);
        //            }
        //            else
        //            {
        //                ticket1.Bookingstatus = "Waitlist";
        //                ticketRepository.TicketCreation(ticket1);
        //                trainRepository.UpdateLadiesCount(t.TrainName);
        //            }
        //        }
        //        return RedirectToAction("ViewTicket", "Ticket");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        //public ActionResult Logout()
        //{
        //    // HttpContext.Session.Abandon();
        //    HttpContext.Session.Clear();
        //    return RedirectToAction("Index", "Home");
        //}
    }
}
