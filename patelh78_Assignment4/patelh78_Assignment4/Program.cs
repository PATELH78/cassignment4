using System;
using System.Collections.Generic;

namespace TicketingConsoleApp
{
    public enum SeatType
    {
        Window,
        Aisle
    }

    public class Seat
    {
        public bool IsOccupied { get; private set; }
        public string PassengerName { get; private set; }

        public Seat()
        {
            IsOccupied = false;
            PassengerName = "";
        }

        public void OccupySeat(string passengerName)
        {
            IsOccupied = true;
            PassengerName = passengerName;
        }

        public void VacateSeat()
        {
            IsOccupied = false;
            PassengerName = "";
        }

        public override string ToString()
        {
            if (IsOccupied)
                return PassengerName;
            else
                return "Available";
        }
    }

    public class Row
    {
        public List<Seat> Seats { get; private set; }

        public Row()
        {
            Seats = new List<Seat>();
            for (int i = 0; i < 4; i++)
            {
                Seats.Add(new Seat());
            }
        }

        public bool IsRowFullyOccupied()
        {
            foreach (var seat in Seats)
            {
                if (!seat.IsOccupied)
                    return false;
            }
            return true;
        }
    }

    public class Airplane
    {
        private List<Row> rows;

        public Airplane()
        {
            rows = new List<Row>();
            for (int i = 0; i < 12; i++)
            {
                rows.Add(new Row());
            }
        }

        public bool BookSeat(string passengerName, SeatType preference = SeatType.Window)
        {
            foreach (var row in rows)
            {
                foreach (var seat in row.Seats)
                {
                    if (!seat.IsOccupied && (preference == SeatType.Window && (seat == row.Seats[0] || seat == row.Seats[3]) || preference == SeatType.Aisle && (seat == row.Seats[1] || seat == row.Seats[2])))
                    {
                        seat.OccupySeat(passengerName);
                        return true;
                    }
                }
            }
            return false;
        }

        public void ShowSeatingArrangement()
        {
            for (int i = 0; i < 12; i++)
            {
                Console.WriteLine($"Row {i + 1}: {rows[i].Seats[0]} {rows[i].Seats[1]} {rows[i].Seats[2]} {rows[i].Seats[3]}");
            }
        }

        public bool IsAirplaneFullyOccupied()
        {
            foreach (var row in rows)
            {
                if (!row.IsRowFullyOccupied())
                    return false;
            }
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Airplane airplane = new Airplane();
            int choice;

            do
            {
                Console.WriteLine("\n1. Book a ticket");
                Console.WriteLine("2. View seating arrangement");
                Console.WriteLine("3. Exit");

                int.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                        BookTicket(airplane);
                        break;
                    case 2:
                        airplane.ShowSeatingArrangement();
                        break;
                    case 3:
                        Console.WriteLine("Exiting the application...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            } while (choice != 3);
        }

        static void BookTicket(Airplane airplane)
        {
            Console.WriteLine("Enter passenger's name:");
            string passengerName = Console.ReadLine();

            Console.WriteLine("Enter 1 for Window seat preference, 2 for Aisle seat preference:");
            int preferenceInput;
            if (!int.TryParse(Console.ReadLine(), out preferenceInput) || (preferenceInput != 1 && preferenceInput != 2))
            {
                Console.WriteLine("Invalid preference. Defaulting to Window.");
                preferenceInput = 1;
            }

            SeatType preference = (preferenceInput == 1) ? SeatType.Window : SeatType.Aisle;

            if (airplane.BookSeat(passengerName, preference))
            {
                Console.WriteLine("Ticket booked successfully!");
            }
            else
            {
                Console.WriteLine("Sorry, the airplane is fully occupied.");
            }
        }
    }
}