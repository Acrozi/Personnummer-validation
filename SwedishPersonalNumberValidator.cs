using System;

namespace grupp_arbete
{
    public class SwedishPersonalNumberValidator
    {
        // Funktion för att validera ett svenskt personnummer.
        public static bool IsValid(string personalNumber)
        {
            // Kontrollera längden på personnumret innan du tar bort bindestrecket.
            if (string.IsNullOrEmpty(personalNumber) || (personalNumber.Length != 10 && personalNumber.Length != 12))
                return false;

            // Ta bort eventuella bindestreck från personnumret.
            string cleanNumber = RemoveHyphen(personalNumber);

            // Kontrollera att personnumret har rätt längd efter att bindestreck har tagits bort (10 eller 12 tecken).
            if (string.IsNullOrEmpty(cleanNumber) || (cleanNumber.Length != 10 && cleanNumber.Length != 12))
                return false;

            // Kontrollera att födelsedatumet är giltigt.
            int dateLength = (cleanNumber.Length == 12) ? 8 : 6; // Bestämmer längden beroende på antalet tecken.
            if (!IsValidBirthDate(cleanNumber.Substring(0, dateLength)))
                return false;

            // Kontrollera kontrollsiffran i personnumret.
            if (!IsValidControlNumber(cleanNumber.Substring(dateLength, 4)))
                return false;

            // Kontrollera åldern baserat på födelsedatumet.
            if (!IsValidAge(cleanNumber.Substring(0, dateLength)))
                return false;

            return true;
        }

        // Funktion för att kontrollera giltigheten av födelsedatumet i personnumret.
        private static bool IsValidBirthDate(string datePart)
        {
            // Kontrollera om strängen är tom eller har en ogiltig längd.
            if (string.IsNullOrEmpty(datePart) || (datePart.Length != 8 && datePart.Length != 10 && datePart.Length != 12))
                return false;

            int year, month, day;

            // Hantera olika format av datum i personnumret baserat på antal tecken.
            if (datePart.Length == 8)
            {
                year = int.Parse(datePart.Substring(0, 2));
                month = int.Parse(datePart.Substring(2, 2));
                day = int.Parse(datePart.Substring(4, 2));
                year += (year < 30) ? 2000 : 1900;  // Hantera 2-siffriga år.
            }
            else if (datePart.Length == 10)
            {
                year = int.Parse(datePart.Substring(0, 4));
                month = int.Parse(datePart.Substring(4, 2));
                day = int.Parse(datePart.Substring(6, 2));
            }
            else
            {
                year = int.Parse(datePart.Substring(0, datePart.Length - 4));
                month = int.Parse(datePart.Substring(datePart.Length - 4, 2));
                day = int.Parse(datePart.Substring(datePart.Length - 2, 2));
                if (datePart.Length == 12) 
                    year += (year < 30) ? 2000 : 1900;  // Hantera 2-siffriga år.
            }

            // Kontrollera att födelsedatumet är giltigt.
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
                int digit = controlNumber[i] - '0';  // Omvandla karaktären till en siffra.
                sum += digit;
            }
            return sum % 10 == 0;  // Kontrollera att summan är delbar med 10.
        }

        // Funktion för att validera åldern baserat på födelsedatumet.
        private static bool IsValidAge(string datePart)
        {
            int year = int.Parse(datePart.Substring(0, 4));
            int currentYear = DateTime.Now.Year;
            // Kontrollera att åldern ligger inom en rimlig gräns.
            if (currentYear - year > 150 || currentYear - year < 0)
                return false;
            return true;
        }

        // Funktion för att hämta kön baserat på personnumret.
// Funktion för att hämta kön baserat på personnumret.
    public static string GetGender(string personalNumber)
    {
    // Kontrollera om personnumret är giltigt med hjälp av IsValid-metoden.

    // Hämta kön baserat på näst sista siffran i personnumret.
    int secondLastDigitIndex = personalNumber.Length - 2;  // Indexet för näst sista siffran.
    char secondLastDigitChar = personalNumber[secondLastDigitIndex];  // Hämtar näst sista siffran som en char.
    
    return (secondLastDigitChar % 2 == 0) ? "Female" : "Male";
}



        // Funktion för att ta bort bindestreck från personnumret.
        private static string RemoveHyphen(string input)
        {
            return input?.Replace("-", "") ?? string.Empty;
        }
    }
}
