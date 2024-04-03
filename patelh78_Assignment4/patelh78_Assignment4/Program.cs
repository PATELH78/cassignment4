using System;
using System.Collections.Generic;

namespace DifferentTicketingConsoleApp
{
    
    enum SeatLabel { A, B, C, D }

  
    class Chair
    {
        public bool IsBooked { get; set; }
        public Passenger Occupant { get; set; }
        public SeatLabel Label { get; }
        public int RowNumber { get; }

        public Chair(int rowNumber, SeatLabel label)
        {
            RowNumber = rowNumber;
            Label = label;
        }
    }

   
    class Traveler
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public SeatLabel PreferredSeat { get; set; }
        public Chair ReservedSeat { get; set; }
    }

    class MyApp
    {
        static Chair[,] seatingLayout = new Chair[12, 4];
        static List<Traveler> travelers = new List<Traveler>();

        static void Main(string[] args)
        {
            InitializeSeatingLayout();

            while (true)
            {
                Console.WriteLine("Please enter 1 to book a ticket.");
                Console.WriteLine("Please enter 2 to see seating chart.");
                Console.WriteLine("Please enter 3 to exit the application.");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        BookTicket();
                        break;
                    case "2":
                        DisplaySeatingChart();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void InitializeSeatingLayout()
        {
            for (int row = 0; row < 12; row++)
            {
                for (int seat = 0; seat < 4; seat++)
                {
                    seatingLayout[row, seat] = new Chair(row + 1, (SeatLabel)seat);
                }
            }
        }

        static void BookTicket()
        {
            Console.WriteLine("Please enter the traveler's first name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Please enter the traveler's last name:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Please enter 1 for a Window seat preference, 2 for an Aisle seat preference, or hit enter to pick the first available seat:");
            string preferenceInput = Console.ReadLine();

            SeatLabel preference = SeatLabel.A;
            if (!string.IsNullOrEmpty(preferenceInput))
            {
                preference = (SeatLabel)(int.Parse(preferenceInput) - 1);
            }

            
            Chair availableSeat = FindAvailableChair(preference);

            if (availableSeat != null)
            {
                Traveler traveler = new Traveler
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PreferredSeat = preference,
                    ReservedSeat = availableSeat
                };

                availableSeat.IsBooked = true;
                availableSeat.Occupant = traveler;

                travelers.Add(traveler);

                Console.WriteLine($"The seat located in {availableSeat.RowNumber} {availableSeat.Label} has been booked.");
            }
            else
            {
                Console.WriteLine("Sorry, the plane is fully booked.");
            }
        }

        static Chair FindAvailableChair(SeatLabel preference)
        {
            foreach (Chair chair in seatingLayout)
            {
                if (!chair.IsBooked)
                {
                    if (preference == SeatLabel.A && (chair.Label == SeatLabel.A || chair.Label == SeatLabel.D))
                    {
                        return chair;
                    }
                    else if (preference == SeatLabel.B && (chair.Label == SeatLabel.B || chair.Label == SeatLabel.C))
                    {
                        return chair;
                    }
                    else if (preference == SeatLabel.C || preference == SeatLabel.D)
                    {
                        return chair;
                    }
                }
            }
            return null;
        }

        static void DisplaySeatingChart()
        {
            for (int row = 0; row < 12; row++)
            {
                for (int seat = 0; seat < 4; seat++)
                {
                    if (seatingLayout[row, seat].IsBooked)
                    {
                        Console.Write($"{seatingLayout[row, seat].Occupant.FirstName.Substring(0, 1)}{seatingLayout[row, seat].Occupant.LastName.Substring(0, 1)} ");
                    }
                    else
                    {
                        Console.Write($"- ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
