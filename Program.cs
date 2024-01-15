using System;

class StartProgram
{
    static void Main()
    {
        Start();
    }

    static void Start()
    {
        Console.WriteLine("Enter a Swedish personal identity number (YYMMDD-XXXX or YYYYMMDD-XXXX):");
        string personalNumber = Console.ReadLine().Replace("-", ""); // Remove hyphen before validation

        // Convert 12-digit format to 10-digit format
        if (personalNumber.Length == 12)
        {
            personalNumber = personalNumber.Substring(2, 10);
        }

        if (SwedishPersonalNumberValidator.IsValid(personalNumber))
        {
            Console.WriteLine("The personal identity number is valid.");

            // Get and display gender
            string gender = SwedishPersonalNumberValidator.GetGender(personalNumber);
            Console.WriteLine($"Gender: {gender}");
        }
        else
        {
            Console.WriteLine("The personal identity number is not valid.");
        }
    }
}

// Assume that SwedishPersonalNumberValidator is a separate class with the necessary methods
