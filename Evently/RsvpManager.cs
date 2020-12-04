using System;
using System.Collections.Generic;
using System.Text;

namespace Evently
{
    class RsvpManager
    {
        private static int id;
        private int numRsvps;
        private int maxNumRsvps;
        private Rsvp[] rsvpList;

        public RsvpManager(int idSeed, int max)
        {
            maxNumRsvps = max;
            numRsvps = 0;
            id = idSeed;
            rsvpList = new Rsvp[maxNumRsvps];
        }
        public bool addRsvp(Event e, Customer c)
        {
            if(numRsvps >= maxNumRsvps) { return false; }
            Rsvp r = new Rsvp(id, e, c);
            rsvpList[numRsvps] = r;
            numRsvps++;
            c.setNumBookings();
            id++;
            return true;
        }
        public int findRsvp(int rid)
        {
            for (int i = 0; i < numRsvps; i++)
            {
                if (rsvpList[i].getRsvpId() == rid)
                {
                    return i;
                }
            }
            return -1;
        }
        public bool rsvpExists(int rid)
        {
            if(findRsvp(rid) == -1) { return false; }
            return true;
        }
        public Rsvp getRsvp(int rid)
        {
            int loc = findRsvp(rid);
            if(loc == -1) { return null; }
            return rsvpList[loc];
        }
        public bool deleteRsvp(int rid)
        {
            int loc = findRsvp(rid);
            if(loc == -1) { return false; }
            rsvpList[loc] = rsvpList[numRsvps - 1];
            numRsvps--;
            return true;
        }
        public string getRsvpInfo(int rid)
        {
            int loc = findRsvp(rid);
            if(loc == -1) { return "There is no Rsvp record with id " + rid +"."; }
            return rsvpList[loc].ToString();
        }
        public Rsvp[] getRsvpObjects()
        {
            if(numRsvps < 1) { return null; }
            return rsvpList;
        }
        public string getRsvpList()
        {
            string s = "Rsvp List";
            s = s + "\nDate \t \t \t Rsvp Number \t Customer \t Event ID";
            for (int i = 0; i < numRsvps; i++)
            {
                Customer c = rsvpList[i].getRsvpCustomer();
                Event e = rsvpList[i].getRsvpEvent();
                s += "\n" + rsvpList[i].getRsvpDate() + "\t " + rsvpList[i].getRsvpId() + "\t \t " + c.getFirstName() + " " + c.getLastName() + "\t " + e.getEventId();
            }
            return s;
        }
    }
}
