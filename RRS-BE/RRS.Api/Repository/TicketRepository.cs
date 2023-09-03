using RRS.Api.Data;
using RRS.Api.Models;
using RRS.Api.ViewModels;

namespace RRS.Api.Repository
{
    public class TicketRepository : ITicketRepository
    {
        ApplicationDbContext context;
        IUserRepository userRepo;

        public TicketRepository(ApplicationDbContext context, IUserRepository userRepository)
        {
            this.context = context;
            userRepo = userRepository;
        }

        public List<TicketDTOoutput> GetTicketInfo(string username)
        {
            try
            {
                var userId = userRepo.GetUserIdAsync(username).Result;
                List<int> passengerIds = context.Passengers.Where(p => p.Id == userId).Select(p => p.PassengerId).ToList();
                List<Ticket> tickets = new List<Ticket>();
                foreach (int pid in passengerIds)
                {
                    tickets.AddRange(context.Tickets.Where(i => i.PassengerId == pid).ToList());
                }
                List<TicketDTOoutput> result = new List<TicketDTOoutput>();
                foreach (var ticket in tickets)
                {
                    result.Add(new TicketDTOoutput
                    {
                        Ticketno = ticket.Ticketno,
                        Ticketclass = ticket.Ticketclass,
                        Berthno = ticket.Berthno,
                        Coachno = ticket.Coachno,
                        Arrivaldate = ticket.Arrivaldate,
                        Bookingdate = ticket.Bookingdate,
                        Bookingstatus = ticket.Bookingstatus,
                        PassengerName = context.Passengers.Where(p => p.PassengerId == ticket.PassengerId).Select(p => p.PassengerName).FirstOrDefault(),
                        TrainNo = ticket.TrainNo
                    });
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void TicketCreation(TicketDTOoutput ticketDto,string UserName)
        {
            try
            {
                string UserId = userRepo.GetUserIdAsync(UserName).Result;
                Ticket ticket = new Ticket
                {
                    //Ticketno = ticketDto.Ticketno,
                    Ticketclass = ticketDto.Ticketclass,
                    Berthno = ticketDto.Berthno,
                    Coachno = ticketDto.Coachno,
                    Arrivaldate = ticketDto.Arrivaldate,
                    Bookingdate = ticketDto.Bookingdate,
                    Bookingstatus = ticketDto.Bookingstatus,
                    PassengerId = context.Passengers.Where(p =>  p.PassengerName == ticketDto.PassengerName && p.Id == UserId).Select(p => p.PassengerId).FirstOrDefault(),
                    TrainNo = ticketDto.TrainNo
                };
                context.Tickets.Add(ticket);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TicketDTOoutput GetTicketByTicketNo(int ticketNo,string username)
        {
            try
            {
                List<TicketDTOoutput> tickets = this.GetTicketInfo(username);
                TicketDTOoutput ticket = tickets.Find(t=>t.Ticketno==ticketNo);
                if (ticket != null)
                {
                 

                    return ticket;
                }
                else
                {
                    throw new Exception("Ticket not found.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void TicketCancel(int Ticketno)
        {
            try
            {
                //string UserId = userRepo.GetUserIdAsync(UserName).Result;


                Ticket ticket = context.Tickets.FirstOrDefault(t => t.Ticketno == Ticketno);
                if (ticket != null)
                {
                    context.Tickets.Remove(ticket);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Ticket not found.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



    }

}

