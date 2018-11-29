using Plugin.ValidationRules.Interfaces;
using System;
using System.Text.RegularExpressions;

namespace Agenda.Validators
{
    public class PersonNames<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;
            
            var regex = new Regex(@"^[A-ZÑáéíóú\s]{5,30}$", RegexOptions.IgnoreCase);

            return regex.IsMatch(str);
        }
    }

}
