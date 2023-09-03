using Microsoft.AspNetCore.Identity;
using RRS.Api.Data;
using RRS.Api.Models;
using RRS.Api.ViewModels;
namespace RRS.Api.Repository
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly ApplicationDbContext _context;
        
        private readonly IUserRepository _userRepository;

        public PassengerRepository(ApplicationDbContext context, IUserRepository userRepository)
        {
            _context = context;
            //_mapper = mapper;
            _userRepository = userRepository;
        }

        

        public void AddPassenger(PassengerDTO passengerDTO, string username)
        {
            try
            {
                Passenger passenger = new Passenger()
                {
                    Gender = passengerDTO.Gender,
                    PassengerName = passengerDTO.PassengerName,
                    Id = _userRepository.GetUserIdAsync(username).Result
                };
                //var passenger = _mapper.Map<Passenger>(passengerDto);
                //passenger.UserId = _userRepository.GetUserIdAsync(passengerDto.User.UserName).Result;
                _context.Passengers.Add(passenger);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PassengerDTO GetPassengerById(int id)
        {
            try
            {
                var passenger = _context.Passengers.FirstOrDefault(p => p.PassengerId == id);
                if (passenger != null)
                {
                    var passengerDTO = new PassengerDTO()
                    {
                        PassengerName = passenger.PassengerName,
                        Gender = passenger.Gender,
                    };
                    return passengerDTO;
                }
                return new PassengerDTO();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PassengerDTO> GetPassengers(string username)
        {
            try
            {
                var userId = _userRepository.GetUserIdAsync(username).Result;
                var passengers = _context.Passengers.Where(p => p.Id == userId).ToList();
                List<PassengerDTO> list = new List<PassengerDTO>();
                foreach(var passenger in passengers)
                {
                    list.Add(new PassengerDTO
                    {
                        PassengerName=passenger.PassengerName,
                        Gender=passenger.Gender,
                    });
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
