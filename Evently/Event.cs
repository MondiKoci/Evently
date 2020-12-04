using System;
using System.Collections.Generic;
using System.Text;

namespace Evently
{
    class Event
    {
        private int eventId;
        private string eventName;
        private int venueId;
        private Date eventDate;
        private int maxAttendees;
        private int numAttendees;
        private string dateCreated;
        private Customer[] attendeeList;

        public Event(int eventId, string name, int venue, Date eventDate, int maxAttendees)
        {
            this.eventId = eventId;
            this.eventName = name;
            this.venueId = venue;
            this.eventDate = eventDate;
            this.maxAttendees = maxAttendees;
            numAttendees = 0;
            attendeeList = new Customer[maxAttendees];
            dateCreated = DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt");
        }

        public int getEventId() { return eventId; }
        public string getEventName() { return eventName; }
        public int getVenueId() { return venueId; }
        public Date getEventDate() { return eventDate; }
        public string getEventCreatedDate() { return dateCreated; }
        public int getMaxAttendees() { return maxAttendees; }
        public int getNumAttendees() { return numAttendees; }
        public bool addAttendee(Customer c)
        {
            if (numAttendees >= maxAttendees) { return false; }
            attendeeList[numAttendees] = c;
            numAttendees++;
            return true;
        }
        private int findAttendee(int custId)
        {
            for (int x = 0; x < maxAttendees; x++)
            {
                if (attendeeList[x].getId() == custId)
                    return x;
            }
            return -1;
        }

        public bool removeAttendee(int custId)
        {
            int loc = findAttendee(custId);
            if (loc == -1) return false;
            attendeeList[loc] = attendeeList[numAttendees - 1];
            numAttendees--;
            return true;
        }

        public string getAttendees()
        {
            string s = "\nCustomers registered:";
            s += "\nID \t Name";
            for (int x = 0; x < numAttendees; x++)
            {
                s = s + "\n"+ attendeeList[x].getId()+ "\t " + attendeeList[x].getFirstName() + " " + attendeeList[x].getLastName();
            }
            return s;
        }

        public override string ToString()
        {
            string s = "Event ID: " + eventId + "\nName: " + eventName;
            s = s + "\nVenue: " + venueId;
            s = s + "\nDate:" + eventDate;
            s = s + "\nRegistered Attendees:" + numAttendees;
            s = s + "\nAvailable spaces:" + (maxAttendees - numAttendees);
            s = s + getAttendees();
            return s;
        }

    }

}
