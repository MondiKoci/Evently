using System;
using System.Collections.Generic;
using System.Text;

namespace Evently
{
    class Customer
    {
        private int customerId;
        private string firstName;
        private string lastName;
        private string phone;
        private int bookings;

        public Customer(int cId, string fname, string lname, string ph)
        {
            this.customerId = cId;
            this.firstName = fname;
            this.lastName = lname;
            this.phone = ph;
            this.bookings = 0;
        }

        public int getId() { return customerId; }
        public string getFirstName() { return firstName; }
        public string getLastName() { return lastName; }
        public string getPhone() { return phone; }
        public void setNumBookings() { bookings++; }
        public int getNumBookings() { return bookings; }
        public override string ToString()
        {
            string s = "Customer ID: " + customerId;
            s = s + "\nName: " + firstName + " " + lastName;
            s = s + "\nPhone: " + phone;
            s = s + "\nBookings: " + bookings;

            return s;
        }

    }

}
