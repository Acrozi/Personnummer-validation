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
        string personalNumber = (Console.ReadLine() ?? "").Trim(); // Remove whitespaces

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
