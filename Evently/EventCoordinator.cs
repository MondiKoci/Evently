using System;
using System.Collections.Generic;
using System.Text;

namespace Evently
{
    class EventCoordinator
    {
        CustomerManager custMan;
        EventManager eventMan;
        VenueManager venMan;
        RsvpManager rsvpMan;

        public EventCoordinator(int custIdSeed, int maxCust, int eventIdSeed, int maxEvents, int venIdSeed, int maxVenues, int rsvpIntSeed, int maxRsvps)
        {
            custMan = new CustomerManager(100, 1000);
            eventMan = new EventManager(eventIdSeed, maxEvents);
            venMan = new VenueManager(venIdSeed, maxVenues);
            rsvpMan = new RsvpManager(rsvpIntSeed, maxRsvps);
        }
        
        //Venue Related methods
        public bool addVenue(string name, int maxBookings)
        {
            return venMan.addVenue(name, maxBookings);
        }
        public string venueList()
        {
            return venMan.getVenueList();
        }
        public string getVenueInfoById(int vid)
        {
            return venMan.getVenueInfo(vid);
        }
        public bool deleteVenue(int vid)
        {
            return venMan.deleteVenue(vid);
        }
        public bool doesVenueExist(int vid)
        {
            if(venMan.findVenue(vid) == -1) { return false; }
            return true;
        }
        public Venue[] venueObjectList()
        {
            return venMan.getVenueObjects();
        }

        //customer related methods
        public bool addCustomer(string fname, string lname, string phone)
        {
            return custMan.addCustomer(fname, lname, phone);
        }

        public string customerList()
        {
            return custMan.getCustomerList();
        }

        public string getCustomerInfoById(int id)
        {
            return custMan.getCustomerInfo(id);
        }
        public bool customerExists(int cid)
        {
            return custMan.customerExist(cid);
        }
        public bool deleteCustomer(int id)
        {
            return custMan.deleteCustomer(id);
        }
        public Customer[] customerObjectList()
        {
            return custMan.getCustomerObjects();
        }

        // Event related methods
        public bool addEvent(string name, int venueId, Date eventDate, int maxAttendee)
        {
            if(!venMan.isVenueAvailable(venueId, eventDate)) { return false; }
            eventMan.addEvent(name, venueId, eventDate, maxAttendee);
            Venue v = venMan.getVenue(venueId);
            return venMan.addBooking(venueId, eventDate);
        }

        public string eventList()
        {
            return eventMan.getEventList();
        }

        public string getEventInfoById(int id)
        {
            return eventMan.getEventInfo(id);
        }
        public bool eventExists(int eid)
        {
            return eventMan.eventExists(eid);
        }
        public Event[] eventObjectList()
        {
            return eventMan.getEventObjects();
        }
        // Rsvp related methods
        public bool makeRsvp(int customerId, int eventId)
        {
            Event e = eventMan.getEvent(eventId);
            Customer c = custMan.getCustomer(customerId);
            if (e.getNumAttendees() < e.getMaxAttendees() 
                && rsvpMan.addRsvp(e, c)){
                return e.addAttendee(c);
            }
            return false;
        }
        public string rsvpList()
        {
            return rsvpMan.getRsvpList();
        }
        public Rsvp[] rsvpObjectList()
        {
            return rsvpMan.getRsvpObjects();
        }

    }

}
