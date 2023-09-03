//using Microsoft.AspNetCore.Identity;
//using RRS.Api.Models;

//namespace RRS.Api.Data
//{
//    public class ApplicationDbInitializer
//    {
//        public static async Task SeedDatabase(ApplicationDbContext context)
//        {
//            if (!context.Trains.Any())
//            {
//                List<Train> list = new List<Train>
//                {
//                    new Train()
//                {
//                    //TrainNo = 1,
//                    TrainName = "Vande Bharat",
//                    Startloc = "Pune ",
//                    Endloc = "Mumbai",
//                    Arrivaltime = "10:00 AM",
//                    Departuretime = "10:05 AM",
//                    Arrivaldate = "2023-06-06",
//                    Ac1tier = 50,
//                    Ac2tier = 50,
//                    Ac3tier = 50,
//                    Sleeper = 200,
//                    Tatkal = 35,
//                    Ladies = 15
//                },
//                new Train()
//                {
//                    //TrainNo = 2,
//                    TrainName = "Garibrath Express",
//                    Startloc = "Nashik ",
//                    Endloc = "Pune",
//                    Arrivaltime = "10:00 AM",
//                    Departuretime = "10:05 AM",
//                    Arrivaldate = "2023-06-05",
//                    Ac1tier = 50,
//                    Ac2tier = 50,
//                    Ac3tier = 50,
//                    Sleeper = 200,
//                    Tatkal = 35,
//                    Ladies = 15
//                },
//                new Train()
//                {
//                    //TrainNo = 3,
//                    TrainName = "Intercity Express",
//                    Startloc = "Mumbai",
//                    Endloc = "Pune",
//                    Arrivaltime = "12:00 PM",
//                    Departuretime = "12:05 PM",
//                    Arrivaldate = "2023-06-02",
//                    Ac1tier = 50,
//                    Ac2tier = 50,
//                    Ac3tier = 50,
//                    Sleeper = 200,
//                    Tatkal = 35,
//                    Ladies = 15
//                },
//                new Train()
//                {
//                    //TrainNo = 4,
//                    TrainName = "Anandvan express",
//                    Startloc = "Nagpur",
//                    Endloc = "Mumbai",
//                    Arrivaltime = "1:00 PM",
//                    Departuretime = "1:05 PM",
//                    Arrivaldate = "2023-06-02",
//                    Ac1tier = 50,
//                    Ac2tier = 50,
//                    Ac3tier = 50,
//                    Sleeper = 200,
//                    Tatkal = 35,
//                    Ladies = 15
//                },
//                new Train()
//                {

//                    //TrainNo = 5,
//                    TrainName = "Shalimar Superfast Express",
//                    Startloc = "Shalimar",
//                    Endloc = "Mumbai",
//                    Arrivaltime = "2:00 PM",
//                    Departuretime = "2:05 PM",
//                    Arrivaldate = "2023-06-02",
//                    Ac1tier = 50,
//                    Ac2tier = 50,
//                    Ac3tier = 50,
//                    Sleeper = 200,
//                    Tatkal = 35,
//                    Ladies = 15
//                },
//                new Train()
//                {

//                    //TrainNo = 6,
//                    TrainName = "Sevagram Express",
//                    Startloc = "Dadar",
//                    Endloc = "Nagpur",
//                    Arrivaltime = "2:00 PM",
//                    Departuretime = "2:05 PM",
//                    Arrivaldate = "2023-06-06",
//                    Ac1tier = 50,
//                    Ac2tier = 50,
//                    Ac3tier = 50,
//                    Sleeper = 200,
//                    Tatkal = 35,
//                    Ladies = 15
//                },
//                new Train()
//                {

//                    //TrainNo = 7,
//                    TrainName = "Maharashtra Express",
//                    Startloc = "Kolhapur",
//                    Endloc = "Mumbai",
//                    Arrivaltime = "10:00 AM",
//                    Departuretime = "10:05 AM",
//                    Arrivaldate = "2023-06-02",
//                    Ac1tier = 50,
//                    Ac2tier = 50,
//                    Ac3tier = 50,
//                    Sleeper = 200,
//                    Tatkal = 35,
//                    Ladies = 15
//                },
//                new Train()
//                {
//                   // TrainNo = 8,
//                    TrainName = "Mahalaxmi Express",
//                    Startloc = "Pune",
//                    Endloc = "Mumbai",
//                    Arrivaltime = "10:00 AM",
//                    Departuretime = "10:05 AM",
//                    Arrivaldate = "2023-06-04",
//                    Ac1tier = 50,
//                    Ac2tier = 50,
//                    Ac3tier = 50,
//                    Sleeper = 200,
//                    Tatkal = 35,
//                    Ladies = 15
//                },
//                new Train()
//                {
//                   // TrainNo = 9,
//                    TrainName = "Deccan Queen",
//                    Startloc = "Pune",
//                    Endloc = "Mumbai",
//                    Arrivaltime = "11:00 AM",
//                    Departuretime = "11:05 AM",
//                    Arrivaldate = "2023-06-02",
//                    Ac1tier = 50,
//                    Ac2tier = 50,
//                    Ac3tier = 50,
//                    Sleeper = 200,
//                    Tatkal = 35,
//                    Ladies = 15
//                },
//                new Train()
//                {
//                    //TrainNo = 10,
//                    TrainName = "Kamini Express",
//                    Startloc = "Mumbai",
//                    Endloc = "Pune",
//                    Arrivaltime = "11:00 AM",
//                    Departuretime = "11:05 AM",
//                    Arrivaldate = "2023-06-08",
//                    Ac1tier = 50,
//                    Ac2tier = 50,
//                    Ac3tier = 50,
//                    Sleeper = 200,
//                    Tatkal = 35,
//                    Ladies = 15
//                }
//                };
//                context.Trains.AddRange(list);
//                context.SaveChanges();
//            }
//        }
//    }
//}
