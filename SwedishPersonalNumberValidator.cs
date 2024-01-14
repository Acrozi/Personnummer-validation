using System;

namespace grupp_arbete
{
    public class SwedishPersonalNumberValidator
    {
        // Funktion för att validera ett svenskt personnummer.
        public static bool IsValid(string personalNumber)
        {
           // Trimma eventuella inledande eller avslutande mellanslag.
            personalNumber = personalNumber?.Trim();

            // Ta bort eventuella bindestreck från personnumret.
            string cleanNumber = RemoveHyphen(personalNumber);
            
            // Kontrollera längden på personnumret.
            if (string.IsNullOrEmpty(personalNumber) || (personalNumber.Length != 13 && personalNumber.Length != 12 && personalNumber.Length != 11 && personalNumber.Length != 10))
            return false;


            // Kontrollera att personnumret har rätt längd efter att bindestreck har tagits bort (10 eller 12 tecken).
            if (string.IsNullOrEmpty(cleanNumber) || (cleanNumber.Length != 10 && cleanNumber.Length != 12))
                return false;

            // Kontrollera att födelsedatumet är giltigt.
            if (!IsValidBirthDate(cleanNumber.Substring(0, 8)))
                return false;

            // Kontrollera kontrollsiffran i personnumret.
            if (!IsValidControlNumber(cleanNumber.Substring(8, 4)))
                return false;

            // Kontrollera åldern baserat på födelsedatumet.
            if (!IsValidAge(cleanNumber.Substring(0, 8)))
                return false;

            return true;
        }

        // Funktion för att kontrollera giltigheten av födelsedatumet i personnumret.
        private static bool IsValidBirthDate(string datePart)
        {
            if (string.IsNullOrEmpty(datePart) || datePart.Length != 8)
                return false;

            int year = int.Parse(datePart.Substring(0, 4));
            int month = int.Parse(datePart.Substring(4, 2));
            int day = int.Parse(datePart.Substring(6, 2));

            if (year < 1900 || year > DateTime.Now.Year || month < 1 || month > 12 || day < 1 || day > DateTime.DaysInMonth(year, month))
                return false;

            return true;
        }

        // Funktion för att validera kontrollsiffran i personnumret.
        private static bool IsValidControlNumber(string controlNumber)
        {
            int sum = 0;
            for (int i = 0; i < 4; i++)
            {
                int digit = controlNumber[i] - '0';
                sum += digit;
            }
            return sum % 10 == 0;
        }

        // Funktion för att validera åldern baserat på födelsedatumet.
        private static bool IsValidAge(string datePart)
        {
            int year = int.Parse(datePart.Substring(0, 4));
            int currentYear = DateTime.Now.Year;
            if (currentYear - year > 150 || currentYear - year < 0)
                return false;
            return true;
        }

        // Funktion för att hämta kön baserat på personnumret.
        public static string GetGender(string personalNumber)
        {
            if (!IsValid(personalNumber))
            {
                throw new ArgumentException("Ogiltigt personnummer", nameof(personalNumber));
            }

            int secondLastDigitIndex = personalNumber.Length - 2;
            char secondLastDigitChar = personalNumber[secondLastDigitIndex];
            
            return (secondLastDigitChar % 2 == 0) ? "Female" : "Male";
        }

        // Funktion för att ta bort bindestreck från personnumret.
        private static string RemoveHyphen(string input)
        {
            return input?.Replace("-", "") ?? string.Empty;
        }
    }
}
