using System;

namespace Model
{
    public static class ValidationFunctions
    {
        const string EmptyError = " ";
        const string DateBirthError = "Wrong birth date";

        public static Func<string, string> ValidateRequiredField = requiredField =>
        {
            string resultMessage = null;

            if (string.IsNullOrEmpty(requiredField?.Trim()))
            {
                resultMessage = EmptyError;
            }

            return resultMessage;
        };

        public static Func<DateTime, string> BirthDateValidete = dateField =>
        {
            string resultMessage = null;

            if (dateField.Date < DateTime.Now.Date)
            {
                resultMessage = DateBirthError;
            }

            return resultMessage;
        };
    }
}
