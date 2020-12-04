using System;
using System.Collections.Generic;
using static System.Console;

namespace Evently
{
    class Program
    {
        static EventCoordinator eCoord;

        //Color methods for displaying messages
        public static void rC() { ForegroundColor = ConsoleColor.Red; }
        public static void wC() { ForegroundColor = ConsoleColor.White; }
        public static void gC() { ForegroundColor = ConsoleColor.Green; }

        //==========Venue-related interaction methods=============
        private static void noVenueMessage()
        {
            if (eCoord.venueObjectList() == null)
            {
                Clear();
                rC(); WriteLine("\nVenue list is empty!"); wC();
                WriteLine("Press any key to return.");
                ReadKey();
                runVenueMenu();
            }
        }
        public static void addVenue()
        {
            string name;
            int maxBookings;
            Clear();
            WriteLine("-----------Add Venue----------");
            Write("\nPlease enter a name for the venue: ");
            name = ReadLine();
            while (!DataValidator.validNaming(name))
            {
                rC();Write("Please enter a valid name for the venue (only alphanumerics allowed): ");wC();
                name = ReadLine();
            }
            Write("Please enter a maxmimum number of bookings allowed for this venue: ");
            maxBookings = getIntChoice();
            while(maxBookings < 1)
            {
                rC(); Write("Please enter a valid number for maximum bookings (minimum 1): "); wC();
                maxBookings = getIntChoice();
            }

            if (eCoord.addVenue(name, maxBookings))
            {
                gC(); WriteLine("Venue addedd successfully.");wC();
            }
            else
            {
                rC(); WriteLine("Venue Could not be added!");wC();

            }
            WriteLine("\nPress any key to continue to return to the menu");
            ReadKey();
        }
        public static void viewVenues()
        {
            noVenueMessage();
            Clear();
            WriteLine(eCoord.venueList());
            WriteLine("\nPress any key to return to the menu");
            ReadKey();
        }
        public static void viewSpecificVenue()
        {
            noVenueMessage();
            int venId;
            string venue;
            Clear();
            WriteLine(eCoord.venueList());
            Write("\nPlease enter id of the venue to view: ");
            venId = getIntChoice();
            while (!eCoord.doesVenueExist(venId))
            {
                rC(); Write("Please enter an existing Venue id: ");wC();
                venId = getIntChoice();
            }
            Clear();
            venue = eCoord.getVenueInfoById(venId);
            WriteLine(venue);
            WriteLine("\nPress any key to continue return to the previous menu.");
            ReadKey();
        }
        public static void deleteVenue()
        {
            noVenueMessage();
            int vid;
            Clear();
            WriteLine(eCoord.venueList());
            Write("\nPlease enter the id of the venue to be deleted:");
            vid = getIntChoice();
            while (!eCoord.doesVenueExist(vid))
            {
                rC(); Write("Please enter an existing Venue id: ");wC();
                vid = getIntChoice();
            }
            eCoord.deleteVenue(vid);
            gC(); WriteLine("Venue with id {0} successfully deleted.", vid);wC();
            WriteLine("\nPress any key to continue return to the main menu.");
            ReadKey();
        }

        //==========Customer-related interaction methods=============
        public static void noCustomerMessage()
        {
            if (eCoord.customerObjectList() == null)
            {
                Clear();
                rC(); WriteLine("\nCustomer list is empty!"); wC();
                WriteLine("Press any key to return");
                ReadKey();
                runCustomerMenu();
            }
        }
        public static void deleteCustomer()
        {
            noCustomerMessage();
            int id;
            Clear();
            WriteLine(eCoord.customerList());
            Write("Please enter a customer id to delete:");
            id = getIntChoice();
            if (eCoord.deleteCustomer(id))
            {
                gC(); WriteLine("Customer with id {0} deleted..", id);wC();
            }
            else
            {
                rC(); WriteLine("Customer with id {0} was not found..", id);wC();
            }
            WriteLine("\nPress any key to continue return to the main menu.");
            ReadKey();
        }


        public static void viewCustomers()
        {
            noCustomerMessage();
            Clear();
            WriteLine(eCoord.customerList());
            WriteLine("\nPress any key to continue return to the main menu.");
            ReadKey();
        }

        public static void viewSpecificCustomer()
        {
            noCustomerMessage();
            int id;
            Clear();
            WriteLine(eCoord.customerList());
            Write("Please enter a customer id to View:");
            id = getIntChoice();
            if (!eCoord.customerExists(id))
            {
                rC();WriteLine("Customer with id {0} does not exist.", id);wC();
            }
            else
            {
                Clear();
                WriteLine(eCoord.getCustomerInfoById(id));
            }
            WriteLine("\nPress any key to continue return to the previous menu.");
            ReadKey();
        }

        public static void addCustomer()
        {
            string fname, lname, phone;

            Clear();
            WriteLine("-----------Add Customer----------");
            Write("\nPlease enter the customer's first name: ");
            fname = ReadLine();
            while (!DataValidator.validNaming(fname))
            {
                rC();Write("Please enter a valid name: ");wC();
                fname = ReadLine();
            }

            Write("Please enter the customer's last name: ");
            lname = ReadLine();

            while (!DataValidator.validNaming(lname))
            {
                rC(); Write("Please enter a valid last name: "); wC();
                lname = ReadLine();
            }

            Write("Please enter the customer's phone (format 123-123-1234): ");
            phone = ReadLine();
            while (!DataValidator.isPhoneNumber(phone))
            {
                rC();Write("Please enter a valid phone number (format 123-123-1234): ");
                phone = ReadLine();
            }

            if (eCoord.addCustomer(fname, lname, phone))
            {
                gC(); WriteLine("Customer successfully added..");wC();
            }
            else
            {
                rC(); WriteLine("Customer was not added..");wC();
            }
            WriteLine("\nPress any key to continue return to the main menu.");
            ReadKey();
        }

        //==========Event-related interaction methods=============
        public static void noEventMessage()
        {
            if (eCoord.eventObjectList() == null)
            {
                Clear();
                rC(); WriteLine("\nEvent list is empty!"); wC();
                Write("Press any key to return to return.");
                ReadKey();
                runEventMenu();
            }
        }
        public static void addEvent()
        {
            noVenueMessage();
            DateTime currentDate = DateTime.Today;
            string eventName;
            Date eventDate;
            int maxAttendees;
            int day, month, year, hour, minute, venueId;

            Clear();
            WriteLine(eCoord.venueList() + "\n");
            WriteLine(eCoord.customerList()+"\n");

            WriteLine("-----------Add Event----------");
            Write("Please enter the name of the Event:");
            eventName = ReadLine();
            Write("Please enter venue id for the event:");
            venueId = getIntChoice();
            while (!eCoord.doesVenueExist(venueId))
            {
                rC();Write("Please enter an existing venue id: ");wC();
                venueId = getIntChoice();
            }

            Write("Please enter the Year of the event:");
            year = getIntChoice();
            while (year < currentDate.Year)
            {
                rC(); Write("Please enter a valid year >= " + currentDate.Year + ": "); wC();
                year = getIntChoice();
            }

            Write("Please enter the Month of the event (as an integer): ");
            month = getIntChoice();
            if (year == currentDate.Year)
            {
                while (month < currentDate.Month || month > 12)
                {
                    rC(); Write("Please enter a valid month >= " + currentDate.Month + ": "); wC();
                    month = getIntChoice();
                }
            }
            else
            {
                while (month < 1 || month > 12)
                {
                    rC(); Write("Please enter a valid month between 1 and 12: "); wC();
                    month = getIntChoice();
                }
            }

            Write("Please enter the Day of the event: ");
            day = getIntChoice();
            if (year == currentDate.Year && month == currentDate.Month)
            {
                while (day <= currentDate.Day || day > 31)
                {
                    rC(); Write("Please enter a valid day > " + currentDate.Day + ": "); wC();
                    day = getIntChoice();
                }
            }
            else
            {
                while (day > 31 || day < 1)
                {
                    rC(); Write("Please enter a valid day between 1 and 31: "); wC();
                    day = getIntChoice();
                }
            }

            Write("Please enter the Hour the event starts in 24 hour format: ");
            hour = getIntChoice();
            while (hour < 0 || hour >= 24)
            {
                rC(); Write("Please enter a valid hour in 24 hour format: "); wC();
                hour = getIntChoice();
            }

            Write("Please enter the Minute the event starts: ");
            minute = getIntChoice();
            while (minute < 0 || minute > 59)
            {
                rC(); Write("Please enter valid minutes from 0 to 59: "); wC();
                minute = getIntChoice();
            }

            eventDate = new Date(day, month, year, hour, minute);
            Write("Please enter the maximum number of attendees:");
            maxAttendees = getIntChoice();
            while(maxAttendees < 1)
            {
                gC();Write("Enter at least one attendee: ");wC();
            }

            if (eCoord.addEvent(eventName, venueId, eventDate, maxAttendees))
            {
                gC(); WriteLine("Event successfully added..");wC();
            }
            else
            {
                rC(); WriteLine("The event was not added..");wC();
            }
            WriteLine("\nPress any key to continue return to the main menu.");
            ReadKey();
        }


        public static void viewEvents()
        {
            noEventMessage();
            Clear();
            WriteLine(eCoord.eventList());
            WriteLine("\nPress any key to continue return to the main menu.");
            ReadKey();
        }

        public static void viewSpecificEvent()
        {
            noEventMessage();
            int id;
            string ev;
            Clear();
            WriteLine(eCoord.eventList());
            Write("\nPlease enter an event id to View:");
            id = getIntChoice();
            Clear();
            ev = eCoord.getEventInfoById(id);
            WriteLine(ev);
            WriteLine("\nPress any key to continue return to the previous menu.");
            ReadKey();
        }

        //==========Rsvp-related interaction methods=============
        public static void noRsvpMessage()
        {
            if (eCoord.rsvpObjectList() == null)
            {
                Clear();
                rC(); WriteLine("\nRsvp list is empty!"); wC();
                WriteLine("Hit any key to return to the main menu");
                ReadKey();
                runProgram();
            }
        }
        public static void viewRsvps()
        {
            noRsvpMessage();
            Clear();
            WriteLine(eCoord.rsvpList());
            WriteLine("\nPress any key to continue return to the main menu.");
            ReadKey();
        }

        public static void makeRsvp()
        {
            noEventMessage();
            int custId;
            int eventId;
            Clear();
            WriteLine(eCoord.eventList() + "\n");
            WriteLine(eCoord.customerList() + "\n");
            WriteLine("-----------Rsvp for an event----------");
            Write("Please enter the Event id to Rsvp: ");
            eventId = getIntChoice();
            while (!eCoord.eventExists(eventId))
            {
                rC();Write("Please enter an existing event id: ");wC();
                eventId = getIntChoice();
            }

            Write("Please enter the Customer id to Rsvp: ");
            custId = getIntChoice();
            while (!eCoord.customerExists(custId))
            {
                rC();Write("Please enter an existing customer id: ");wC();
                custId = getIntChoice();
            }
            if (!eCoord.makeRsvp(custId, eventId))
            {
                rC(); WriteLine("Could not RSVP.."); wC();
            }
            else { gC(); WriteLine("Customer with id {0} rsvp'd successfully for event with id {1}", custId, eventId); wC(); }

            WriteLine("\nPress any key to continue return to the previous menu.");
            ReadKey();
        }

        /*=================Menu methods=================*/
        public static string venueMenu()
        {
            string s = "Andrew's Event Management Limited.\n";
            s += "Venue Menu.\n";
            s += "Please select a choice from the menu below:\n";
            s += "1: Add Venue \n";
            s += "2: View Venues \n";
            s += "3: View Specific Venue \n";
            s += "4: Delete Venue \n";
            s += "5: Return to the main menu.";
            return s;
        }

        public static string customerMenu()
        {
            string s = "Andrew's Event Management Limited.\n";
            s += "Customer Menu.\n";
            s += "Please select a choice from the menu below:\n";
            s += "1: Add Customer \n";
            s += "2: View Customers \n";
            s += "3: View Customer Details \n";
            s += "4: Delete Customer\n";
            s += "5: Return to the main menu.";
            return s;
        }

        public static string eventMenu()
        {
            string s = "Andrew's Event Management Limited.\n";
            s += "Event Menu.\n";
            s += "Please select a choice from the menu below:\n";
            s += "1: Add Event \n";
            s += "2: View all Events \n";
            s += "3: View Event Details \n";
            s += "4: Return to the main menu.";
            return s;
        }

        public static string registrationMenu()
        {
            string s = "Andrew's Event Management Limited.\n";
            s += "Event Registration Menu.\n";
            s += "Please select a choice from the menu below:\n";
            s += "1: RSVP for event \n";
            s += "2: View RSVPs \n";
            s += "3: Return to the main menu.";
            return s;
        }

        public static string mainMenu()
        {
            string s = "Welcome to EVENTLY - Event Management System.\n";
            s += "Please select a choice from the menu below:\n";
            s += "1: Venue Options \n";
            s += "2: Customer Options \n";
            s += "3: Event Options \n";
            s += "4: RSVP for Event \n";
            s += "5: Exit";
            return s;
        }

        public static void runVenueMenu()
        {
            string menu = venueMenu();
            int choice = getValidChoice(5, menu);
            while (choice != 5)
            {
                if (choice == 1) { addVenue(); }
                if (choice == 2) { viewVenues(); }
                if (choice == 3) { viewSpecificVenue(); }
                if (choice == 4) { deleteVenue(); }
                choice = getValidChoice(5, menu);
            }
        }

        public static void runCustomerMenu()
        {
            string menu = customerMenu();
            int choice = getValidChoice(5, menu);
            while (choice != 5)
            {
                if (choice == 1) { addCustomer(); }
                if (choice == 2) { viewCustomers(); }
                if (choice == 3) { viewSpecificCustomer(); }
                if (choice == 4) { deleteCustomer(); }
                choice = getValidChoice(5, menu);
            }
        }

        public static void runEventMenu()
        {
            string menu = eventMenu();
            int choice = getValidChoice(4, menu);
            while (choice != 4)
            {
                if (choice == 1) { addEvent(); }
                if (choice == 2) { viewEvents(); }
                if (choice == 3) { viewSpecificEvent(); }

                choice = getValidChoice(4, menu);
            }
        }

        public static void runRegistrationMenu()
        {
            string menu = registrationMenu();
            int choice = getValidChoice(3, menu);
            while (choice != 3)
            {
                if (choice == 1) { makeRsvp(); }
                if (choice == 2) { viewRsvps();}

                choice = getValidChoice(3, menu);
            }
        }


        public static int getValidChoice(int max, string menu)
        {
            int choice;
            Clear();
            WriteLine(menu);
            while (!int.TryParse(ReadLine(), out choice) || (choice < 1 || choice > max))
            {
                Clear();
                WriteLine(menu);
                rC(); WriteLine("Please enter a valid choice: ");wC();
            }
            return choice;
        }

        public static int getIntChoice()
        {
            int choice;
            while (!int.TryParse(ReadLine(), out choice))
            {
                rC(); WriteLine("Please enter an integer: ");wC();
            }
            return choice;
        }


        public static void runProgram()
        {
            string menu = mainMenu();
            int choice = getValidChoice(5, menu);
            while (choice != 5)
            {
                if (choice == 1) { runVenueMenu(); }
                if (choice == 2) { runCustomerMenu(); }
                if (choice == 3) { runEventMenu(); }
                if (choice == 4) { runRegistrationMenu(); }

                choice = getValidChoice(5, menu);
            }
        }

        static void Main(string[] args)
        {
            eCoord = new EventCoordinator(200, 1000, 101, 5000, 10, 30, 1, 1000);
            runProgram();
            gC();  WriteLine("\nThank you for using EVENTLY");wC();
            WriteLine("Press any key to exit.");

            /*==========Testing Storage IO For customers===========*/
            //int id = StorageIO.getId("cid_storage.txt");
            //int id2 = StorageIO.getId("cid_storage.txt");
            //Customer a = new Customer(id, "Mondi", "Koci", "647-395-9281", 2);
            //Customer b = new Customer(id2, "Edmond", "Chochi", "647-395-9281");
            //StorageIO.addCustomers(a);
            //StorageIO.addCustomers(b);

            //List<Customer> clist = new List<Customer>();
            //clist = StorageIO.GetCustomers();
            //for (int x = 0; x < clist.Count; x++)
            //{
            //    WriteLine(clist[x].ToString());
            //}

            /*============Testing CustomerManager's changed methods==========*/
            //CustomerManager cm = new CustomerManager();
            //WriteLine(cm.getCustomerList());
            //cm.addCustomer("Mondi", "Koci", "647-395-9281");

            /*============Testing Date class======================*/
            //Date a = new Date(12, 12, 12, 12, 12);
            //Date b = new Date(12, 12, 12, 12, 12);
            //if(a == b) { Write("Yes"); }
            //else { WriteLine("No"); }
            // This returns false nevertheless that two objects have similar data

            /*============Testing Venue classes====================*/
            //VenueManager vm = new VenueManager(100, 100);
            //vm.addVenue("City Hall", 100);
            //vm.addVenue("Bday Hall", 100);
            //WriteLine(vm.getVenueList());
            //vm.deleteVenue(101);
            //WriteLine("=====================");
            //WriteLine(vm.getVenueList());

            /*============Testing DataValidator classe====================*/
            //if (DataValidator.validNaming("Hello123")) { WriteLine("Yes"); }
            //else { WriteLine("No"); }





            ReadKey();
        }

    }

}
