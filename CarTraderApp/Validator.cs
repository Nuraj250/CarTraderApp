using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarTraderApp
{
    internal class Validator
    {
        public static class ValidatorFeilds
        {
            // Validates the email format
            public static bool IsValidEmail(string email)
            {
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, emailPattern);
            }

            // Validates the username format
            public static bool IsValidUsername(string username)
            {
                string usernamePattern = @"^[a-zA-Z0-9]{3,15}$";
                return Regex.IsMatch(username, usernamePattern);
            }

            // Validates the password format
            public static bool IsValidPassword(string password)
            {
                string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$";
                return Regex.IsMatch(password, passwordPattern);
            }
        }
    }
}
