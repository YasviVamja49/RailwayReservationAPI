using RRS.Api.Models;
using RRS.Api.ViewModels;

namespace RRS.Api.Repository
{
    public interface IPassengerRepository
    {
        List<PassengerDTO> GetPassengers(string username);
        void AddPassenger(PassengerDTO passengerDto, string username);
        PassengerDTO GetPassengerById(int id);
    }

    public interface IUserRepository
    {
        Task<string> GetUserIdAsync(string username);
    }

    public interface ITicketRepository
    {
        void TicketCreation(TicketDTOoutput ticketDto,String UserName);
        List<TicketDTOoutput> GetTicketInfo(string username);
        void TicketCancel(int Ticketno);
        TicketDTOoutput GetTicketByTicketNo(int ticketNo, string username);
    }


    public interface ITrainRepository
    {
        void AddTrain(TrainDTO trainDTO);
        List<TrainDTO> GetAllTrains();
        void DeleteTrain(TrainDTO trainDTO);
        void EditTrain(TrainDTO trainDTO);
        TrainDTO GetTrainInfo(int trainid);
        int GetSeatCount(string trainName, string seatClass);
        void UpdateSeatCount(string trainName, string seatClass);
        void UpdateSeatCountOnCancel(int trainno, string seatclass);
        TrainDTO GetTrainByName(string trainName);
    }


}
