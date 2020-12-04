using System;
using System.Collections.Generic;
using System.Text;

namespace Evently
{
    class Rsvp
    {
        private int id;
        private string rsvpDate;
        private Event rsvpEvent;
        private Customer rsvpCustomer;

        public Rsvp(int id, Event rsvpEvent, Customer rsvpCustomer)
        {
            this.id = id;
            this.rsvpDate = DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt");
            this.rsvpEvent = rsvpEvent;
            this.rsvpCustomer = rsvpCustomer;
        }
        public int getRsvpId() { return id; }
        public string getRsvpDate() { return rsvpDate; }
        public Event getRsvpEvent() { return rsvpEvent; }
        public Customer getRsvpCustomer() { return rsvpCustomer; }

        public override string ToString()
        {
            string s = "Rsvp ID: " + id;
            s += "\nRsvp Date: " + rsvpDate;
            s += "\nEvent ID: " + rsvpEvent.getEventId();
            s += "\nCustomer ID: " + rsvpCustomer.getId();

            return s;
        }
    }
}
