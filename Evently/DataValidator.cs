using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Evently
{
    class DataValidator
    {   //Validate names/last names
        public static bool validNaming(string input)
        {
            Regex r = new Regex("^[a-zA-Z0-9, '-]*$");
            input = input.Trim(' ');
            if (r.IsMatch(input) && input.Length != 0) { return true; }
            return false;
        }
        //Validate phone number
        public static bool isPhoneNumber(string phoneNumber)
        {
            return string.IsNullOrWhiteSpace(phoneNumber) == false
                && Regex.Match(phoneNumber, @"^(\d{3}-\d{3}-\d{4})$").Success;
        }
    }
}
