using Microsoft.EntityFrameworkCore;
using RRS.Api.Data;
using RRS.Api.Models;
using RRS.Api.ViewModels;

namespace RRS.Api.Repository
{
    public class TrainRepository : ITrainRepository
    {
        ApplicationDbContext context;
        IUserRepository userRepo;
        public TrainRepository(ApplicationDbContext context)
        {
            this.context = context;
            //userRepo=userRepository;
        }
        public void AddTrain(TrainDTO TrainDTO)
        {
            try
            {
                var train = new Train
                {
                    TrainName = TrainDTO.TrainName,
                    Startloc = TrainDTO.Startloc,
                    Endloc = TrainDTO.Endloc,
                    Arrivaltime = TrainDTO.Arrivaltime,
                    Departuretime = TrainDTO.Departuretime,
                    Arrivaldate = TrainDTO.Arrivaldate,
                    Ac1tier = TrainDTO.Ac1tier,
                    Ac2tier = TrainDTO.Ac2tier,
                    Ac3tier = TrainDTO.Ac3tier,
                    Sleeper = TrainDTO.Sleeper,
                    Tatkal = TrainDTO.Tatkal,
                    Ladies = TrainDTO.Ladies,
                    BaseFare = TrainDTO.BaseFare
                };

                context.Trains.Add(train);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TrainDTO> GetAllTrains()
        {
            try
            {
                var trains = context.Trains.ToList();
                var TrainDTOs = trains.Select(train => new TrainDTO
                {
                    TrainNo = train.TrainNo,
                    TrainName = train.TrainName,
                    Startloc = train.Startloc,
                    Endloc = train.Endloc,
                    Arrivaltime = train.Arrivaltime,
                    Departuretime = train.Departuretime,
                    Arrivaldate = train.Arrivaldate,
                    Ac1tier = train.Ac1tier,
                    Ac2tier = train.Ac2tier,
                    Ac3tier = train.Ac3tier,
                    Sleeper = train.Sleeper,
                    Tatkal = train.Tatkal,
                    Ladies = train.Ladies,
                    BaseFare = train.BaseFare
                }).ToList();

                return TrainDTOs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteTrain(TrainDTO train)
        {
            try
            {
                Train trainEntity = context.Trains.Find(train.TrainNo);
                if (trainEntity != null)
                {
                    context.Trains.Remove(trainEntity);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void EditTrain(TrainDTO TrainDTO)
        {
            try
            {
                Train train = context.Trains.Find(TrainDTO.TrainNo);
                train.TrainName = TrainDTO.TrainName;
                train.Startloc = TrainDTO.Startloc;
                train.Endloc = TrainDTO.Endloc;
                train.Arrivaltime = TrainDTO.Arrivaltime;
                train.Departuretime = TrainDTO.Departuretime;
                train.Arrivaldate = TrainDTO.Arrivaldate;
                train.Ac1tier = TrainDTO.Ac1tier;
                train.Ac2tier = TrainDTO.Ac2tier;
                train.Ac3tier = TrainDTO.Ac3tier;
                train.Sleeper = TrainDTO.Sleeper;
                train.Tatkal = TrainDTO.Tatkal;
                train.Ladies = TrainDTO.Ladies;
                train.BaseFare = TrainDTO.BaseFare;

                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public TrainDTO GetTrainInfo(int trainid)
        {
            try
            {
                Train train = context.Trains.Find(trainid);
                if (train == null) 
                    return new TrainDTO(); // train not found

                return new TrainDTO
                {
                    TrainNo = train.TrainNo,
                    TrainName = train.TrainName,
                    Startloc = train.Startloc,
                    Endloc = train.Endloc,
                    Arrivaltime = train.Arrivaltime,
                    Departuretime = train.Departuretime,
                    Arrivaldate = train.Arrivaldate,
                    Ac1tier = train.Ac1tier,
                    Ac2tier = train.Ac2tier,
                    Ac3tier = train.Ac3tier,
                    Sleeper = train.Sleeper,
                    Tatkal = train.Tatkal,
                    Ladies = train.Ladies,
                    BaseFare=train.BaseFare
                };
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int GetSeatCount(string trainname, string seatclass)
        {
            try
            {
                var train = context.Trains.First(t => t.TrainName.Equals(trainname));
                int count = 0;
                switch (seatclass.ToLower())
                {
                    case "ac1tier":
                        count = train.Ac1tier;
                        break;
                    case "ac2tier":
                        count = train.Ac2tier;
                        break;
                    case "ac3tier":
                        count = train.Ac3tier;
                        break;
                    case "sleeper":
                        count = train.Sleeper;
                        break;
                    case "tatkal":
                        count = train.Tatkal;
                        break;
                    case "ladies":
                        count = train.Ladies;
                        break;
                    default:
                        throw new ArgumentException("Invalid seat class: " + seatclass);
                }
                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateSeatCount(string trainname, string seatclass)
        {
            try
            {
                var train = context.Trains.First(t => t.TrainName.Equals(trainname));
                switch (seatclass.ToLower())
                {
                    case "ac1tier":
                        train.Ac1tier -= 1;
                        break;
                    case "ac2tier":
                        train.Ac2tier -= 1;
                        break;
                    case "ac3tier":
                        train.Ac3tier -= 1;
                        break;
                    case "sleeper":
                        train.Sleeper -= 1;
                        break;
                    case "tatkal":
                        train.Tatkal -= 1;
                        break;
                    case "ladies":
                        train.Ladies -= 1;
                        break;
                    default:
                        throw new ArgumentException("Invalid seat class: " + seatclass);
                }
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void UpdateSeatCountOnCancel(int trainno, string seatclass)
        {
            try
            {
                var train = context.Trains.First(t => t.TrainNo==trainno);
                switch (seatclass.ToLower())
                {
                    case "ac1tier":
                        train.Ac1tier += 1;
                        break;
                    case "ac2tier":
                        train.Ac2tier += 1;
                        break;
                    case "ac3tier":
                        train.Ac3tier += 1;
                        break;
                    case "sleeper":
                        train.Sleeper += 1;
                        break;
                    case "tatkal":
                        train.Tatkal += 1;
                        break;
                    case "ladies":
                        train.Ladies += 1;
                        break;
                    default:
                        throw new ArgumentException("Invalid seat class: " + seatclass);
                }
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public TrainDTO GetTrainByName(string trainName)
        {
            try
            {
                Train train = context.Trains.First(t => t.TrainName.Equals(trainName));
                return new TrainDTO
                {
                    TrainNo = train.TrainNo,
                    TrainName = train.TrainName,
                    Startloc = train.Startloc,
                    Endloc = train.Endloc,
                    Arrivaltime = train.Arrivaltime,
                    Departuretime = train.Departuretime,
                    Arrivaldate = train.Arrivaldate,
                    Ac1tier = train.Ac1tier,
                    Ac2tier = train.Ac2tier,
                    Ac3tier = train.Ac3tier,
                    Sleeper = train.Sleeper,
                    Tatkal = train.Tatkal,
                    Ladies = train.Ladies,
                    BaseFare = train.BaseFare
                };
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
