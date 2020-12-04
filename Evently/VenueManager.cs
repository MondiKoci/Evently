using System;
using System.Collections.Generic;
using System.Text;

namespace Evently
{
    class VenueManager
    {
        private static int currentVenueId;
        private int maxNumVenues;
        private int numVenues;
        private Venue[] venueList;

        public VenueManager(int idSeed, int max)
        {
            currentVenueId = idSeed;
            maxNumVenues = max;
            numVenues = 0;
            venueList = new Venue[maxNumVenues];
        }
        public bool addVenue(string name, int maxBookings)
        {
            if (numVenues >= maxNumVenues) { return false; }
            Venue v = new Venue(currentVenueId, name, maxBookings);
            venueList[numVenues] = v;
            numVenues++;
            currentVenueId++;
            return true;
        }
        public int findVenue(int id)
        {
            for(int x = 0; x < numVenues; x++)
            {
                if(venueList[x].getId() == id)
                {
                    return x;
                }
            }
            return -1;
        }
        public bool deleteVenue(int vid)
        {
            int loc = findVenue(vid);
            if(loc == -1) { return false; }
            venueList[loc] = venueList[numVenues - 1];
            numVenues--;
            return true;
        }
        public string getVenueInfo(int vid)
        {
            int loc = findVenue(vid);
            if(loc == -1) { return "There is no venue with id " + vid + "."; }
            return venueList[loc].ToString();
        }
        public Venue getVenue(int vid)
        {
            int loc = findVenue(vid);
            if(loc == -1) { return null; }
            return venueList[loc];
        }
        public bool isVenueAvailable(int vid, Date date)
        {
            Venue v = getVenue(vid);
            if(v.getNumBookings() >= v.getMaxBookings()) { return false; }
            if(v.getNumBookings() < 1) { return true; }

            Date[] bookingsList = v.getBookings();
            for (int x = 0; x < v.getNumBookings(); x++)
            {
                Date booked = bookingsList[x];
                if (booked.year == date.year)
                {
                    if(booked.day == date.day) { return false; }
                }
            }
            return true;
        }
        public Venue[] getVenueObjects() {
            if (numVenues < 1) { return null; }
            return venueList;
        }
        public bool addBooking(int vid, Date date)
        {
            int loc = findVenue(vid);
            if(loc == -1) { return false; }
            return venueList[loc].addBooking(date);
        }
        public string getVenueList()
        {
            string s = "Venue List:";
            s += "\nVenue Id \t Venue Name \t Bookings";
            for(int x = 0; x < numVenues; x++)
            {
                s += "\n" + venueList[x].getId() + "\t \t " + venueList[x].getName() + "\t "+venueList[x].getNumBookings();
            }
            return s;
        }
    }
}
