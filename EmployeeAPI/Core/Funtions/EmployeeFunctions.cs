using EmployeeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeAPI.Core.Funtions
{
    public static class EmployeeFunctions
    {
        /// <summary>
        /// Validate Employee object before saving to the database
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public static List<string> ValidateEmployee(Employee employee)
        {
            List<string> errors = new List<string>();

            if (Tools.Validaters.CleanString(employee.FirstName) == "")
                errors.Add("first name is required");
            if (!Tools.Validaters.ValidateEmail(employee.Email))
                errors.Add("invalid email");

            string gender = Tools.Validaters.CleanString(employee.Gender);
            if (!(new[] { "F", "M" }).ToList().Contains(gender, StringComparer.OrdinalIgnoreCase))
                errors.Add("invalid gender please enter 'M' or 'F'");

            if (Tools.Validaters.CleanDecimal(employee.Salary) == 0)
                errors.Add("invalid salary record please enter a decimal number");

            if (!Tools.Validaters.Validatedate(employee.HiredDate))
                errors.Add("invalid hired date");

            return errors;
        }
    }
}
