using System;

class PersonnummerValidator
{
    public static bool IsValidPersonalNumber(string personalNumber)
    {
        // Ta bort bindestreck om de finns
        personalNumber = personalNumber.Replace("-", "");

       // Konvertera 12-siffrigt format till 10-siffrigt format
        if (personalNumber.Length == 12)
        {
            personalNumber = personalNumber.Substring(2, 10);
        } 

        // Check if the length is correct
        if (personalNumber.Length != 10)
        {
            return false;
        }

        // Extract the birthdate part based on the format
        string birthdatePart = personalNumber.Substring(0, 6);

        // Extract the serial number part
        string serialNumberPart = personalNumber.Substring(personalNumber.Length - 4, 3);

        // Extract the checksum
        int checksum = int.Parse(personalNumber.Substring(personalNumber.Length - 1, 1));

        // Concatenate the birthdate and serial number for checksum calculation
        string fullNumberForChecksum = birthdatePart + serialNumberPart;

        // Calculate the checksum
        int calculatedChecksum = CalculateChecksum(fullNumberForChecksum);

        // Check if the calculated checksum matches the provided checksum
        return checksum == calculatedChecksum;
    }

    public static string GetGender(string personalNumber)
    {
        // Ta bort eventuella bindestreck
        personalNumber = personalNumber.Replace("-", "");

        // Kontrollera om personnumret är giltigt innan du fortsätter
        if (!IsValidPersonalNumber(personalNumber))
        {
            throw new ArgumentException("Invalid personal identity number or gender", nameof(personalNumber));
        }

        // Hämta den näst sista siffran (index 8 för 10-siffrigt format)
        char secondToLastDigit = personalNumber[personalNumber.Length - 2];

        // Avgör kön baserat på den näst sista siffran
        return (secondToLastDigit % 2 == 0) ? "Female" : "Male";
    }

    static int CalculateChecksum(string number)
    {
        int sum = 0;

        // Multiply each digit by 2 for odd positions
        for (int i = 0; i < number.Length; i++)
        {
            int digit = int.Parse(number[i].ToString());
            int multiplier = (i % 2 == 0) ? 2 : 1;

            int product = digit * multiplier;

            // Add the digits of the product
            sum += product / 10 + product % 10;
        }

        // Calculate the checksum
        int checksum = (10 - (sum % 10)) % 10;

        return checksum;
    }
}