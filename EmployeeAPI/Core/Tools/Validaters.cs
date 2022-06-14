using System;

namespace EmployeeAPI.Core.Tools
{
    internal static class Validaters
    {
        /// <summary>
        /// Validate Email Address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        internal static bool ValidateEmail(string email) {

            if (email == null) return false;

            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith(".")) return false;
            
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validate Dates
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        internal static bool Validatedate(DateTime? date)
        {
            if (date == null) return false;

            try
            {
                DateTime temp;
                if (DateTime.TryParse(date.ToString(), out temp)) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Clean string value and check for null values
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        internal static string CleanString(object val)
        {
            if (val == null) return "";

            return val.ToString();
        }

        /// <summary>
        /// Validating decimal value
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        internal static decimal CleanDecimal(object val)
        {
            if (val == null) return 0;
            return Convert.ToDecimal(val);
        }
    }
}
