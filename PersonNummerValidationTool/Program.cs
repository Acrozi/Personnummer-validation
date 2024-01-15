using System;

class StartProgram
{
    static void Main()
    {
        while (true)
        {
            if (Start())
            {
                break; // Avsluta loopen om Start() returnerar true
            }
        }
    }

    static bool Start()
    {
        Console.WriteLine("Enter a Swedish personal identity number (YYMMDD-XXXX or YYYYMMDD-XXXX):");
        string personalNumber = (Console.ReadLine() ?? "").Trim(); // Remove whitespaces

        if (personalNumber.Equals("Q", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Exiting program...");
            return true; // Returnera true för att avsluta loopen
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

        return false; // Returnera false för att fortsätta loopen
    }
}
