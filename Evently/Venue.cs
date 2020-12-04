using System;
using System.Collections.Generic;
using System.Text;

namespace Evently
{
    class Venue
    {
        private int id;
        private string name;
        private int maxBookings;
        private int numBookings;
        private Date[] bookings;

        public Venue(int id, string name, int max)
        {
            this.id = id;
            this.name = name;
            this.maxBookings = max;
            bookings = new Date[maxBookings];
        }
        public int getId() { return id; }
        public string getName() { return name; }
        public bool addBooking(Date date)
        {
            if(numBookings >= maxBookings) { return false; }
            bookings[numBookings] = date;
            numBookings++;
            return true;
        }
        public int getMaxBookings() { return maxBookings; }
        public int getNumBookings() { return numBookings; }
        public Date[] getBookings() { return bookings; }
        public override string ToString()
        {
            string s = "Venue ID: " + id;
            s += "\nVenue name: " + name;
            s += "\nNumber of bookings: " + numBookings;
            return s;
        }
    }
}
