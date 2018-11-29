using Plugin.ValidationRules.Interfaces;
using System.Text.RegularExpressions;

namespace Agenda.Validators
{
    public class Cellphone<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;
            var regex = new Regex(@"09[0-9]{8}", RegexOptions.IgnoreCase);

            return regex.IsMatch(str);
        }
    }

}
