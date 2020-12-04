using System;
using System.Collections.Generic;
using System.Text;

namespace Evently
{
    class EventManager
    {
        private static int currentEventId;
        private int maxEvents;
        private int numEvents;
        private Event[] eventList;

        public EventManager(int idSeed, int max)
        {
            currentEventId = idSeed;
            maxEvents = max;
            numEvents = 0;
            eventList = new Event[maxEvents];
        }

        public bool addEvent(string name, int venueId, Date eventDate, int maxAttendees)
        {
            if (numEvents >= maxEvents) { return false; }
            Event e = new Event(currentEventId, name, venueId, eventDate, maxAttendees);
            eventList[numEvents] = e;
            numEvents++;
            currentEventId++;
            return true;
        }

        private int findEvent(int eid)
        {
            for (int x = 0; x < numEvents; x++)
            {
                if (eventList[x].getEventId() == eid)
                    return x;
            }
            return -1;
        }

        public bool eventExists(int eid)
        {
            int loc = findEvent(eid);
            if (loc == -1) { return false; }
            return true;
        }

        public Event getEvent(int eid)
        {
            int loc = findEvent(eid);
            if (loc == -1) { return null; }
            return eventList[loc];
        }

        public bool deleteEvent(int eid)
        {
            int loc = findEvent(eid);
            if (loc == -1) { return false; }
            eventList[loc] = eventList[numEvents - 1];
            numEvents--;
            return true;
        }
        public Event[] getEventObjects()
        {
            if (numEvents < 1) { return null; }
            return eventList;
        }
        public string getEventInfo(int eid)
        {
            int loc = findEvent(eid);
            if (loc == -1) { return "There is no event with id " + eid + "."; }
            return eventList[loc].ToString();
        }
        public string getEventList()
        {
            string s = "Event List";
            s += "\nID \t Event Name \t Venue ID";
            for (int x = 0; x < numEvents; x++)
            {
                s = s + "\n" + eventList[x].getEventId() + " \t " + eventList[x].getEventName() + " \t " + eventList[x].getVenueId();
            }
            return s;
        }

    }

}
