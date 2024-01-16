![example workflow](https://github.com/Acrozi/Personnummer-validation/actions/workflows/docker.yml/badge.svg)![example workflow](https://github.com/Acrozi/Personnummer-validation/actions/workflows/tests.yml/badge.svg)
# To-do

# Personnummer Verification Console Application

## Introduktion

Det här projektet är en C#-baserad konsolapplikation för att verifiera svenska personnummer. 
Nedan finner du dokumentation om hur du kan köra och testa applikationen både lokalt och genom att använda Docker-container.

## Lokal Körning och Testning

För att köra och testa applikationen lokalt, följ stegen nedan:

1. ## Klona projektet från GitHub:
    bash
    git clone https://github.com/Acrozi/Personnummer-validation.git

2. # Navigera till projektmappen:
    bash
    cd Personnummer-validation

3. # Öppna projektet i din C#-utvecklingsmiljö t.ex., Visual Studio eller VS Code.

4. # Bygg projektet:
    bash
    dotnet build

5. # Kör applikationen:
    bash
    dotnet run

6. # Använd applikationen för att verifiera personnummer:
    Följ instruktionerna som visas i konsolen för att ange ett svenskt personnummer och se resultatet av verifieringen.


7. # Kör enhetstester:
    bash
    dotnet test


Se resultaten av enhet testerna för att säkerställa korrekt funktionalitet.

## Körning med Docker

För att köra applikationen med Docker, så måste du kunna lära dig dom olika alternativ som finns,
Att först köra med docker container så måste man ha installerat docker på sin dator, efteråt hämtar man en docker image med hjälp av
Här är OPTIONS för de olika alternativen som kan användas med docker exec-kommandot, CONTAINER är namnet eller ID på behållaren där kommandot ska köras, COMMAND är kommandot som ska köras och ARG är argumentet. passerade att ge order till kommandot.









###########################################################################################

Niklas del

## Personnummer

Personnummer är ett nummer som Skatteverket tilldelar personer folkbokförda i Sverige för att identifiera dem hos bland annat myndigheter. Systemet, som var det första i världen som omfattade ett lands hela befolkning, infördes 1 januari 1947.

Ett personnummer är uppbyggt av 10 siffror indelade i två grupper om 6 respektive 4 siffror. Grupperna är åtskilda med ett skiljetecken, normalt ett bindestreck (-).

Ett personnummer kan matas in på olika sätt, bland annat genom 12 siffror, 10 siffror med bindestreck eller 10 siffror utan bindestreck.

För att kontrollera detta använder vi funktionen `IsValid`:

```csharp
    public static bool IsValid(string personalNumber)
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
```
Genom att kontrollera den nionde siffran i personnumret kan vi avgöra om personen är en man eller en kvinna, jämn siffra för kvinnor och udda siffra för män. 
Detta kontrollerar vi genom funktionen `GetGender`:

    
```csharp
       public static string GetGender(string personalNumber)
    {
        // Ta bort eventuella bindestreck
        personalNumber = personalNumber.Replace("-", "");

        // Kontrollera om personnumret är giltigt innan du fortsätter
        if (!IsValid(personalNumber))
        {
            throw new ArgumentException("Invalid personal identity number or gender", nameof(personalNumber));
        }

        // Hämta den näst sista siffran (index 8 för 10-siffrigt format)
        char secondToLastDigit = personalNumber[personalNumber.Length - 2];

        // Avgör kön baserat på den näst sista siffran
        return (secondToLastDigit % 2 == 0) ? "Female" : "Male";
    }
```
Personnummer kan beräknas för att se om det är äkta. Detta görs genom att man multiplicerar de 9 första siffrorna med omväxlande 2 och 1.
De respektive siffersummorna adderas. Om man adderar kontrollsiffran(sista siffran) till denna summa skall man få ett tal jämt delbart med 10.
Detta kontrollerar vi genom funktionen `CalculateChecksum`:


```csharp
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
```
## Collaborators
<a href="https://github.com/niklasallard1">
  <img src="https://github.com/niklasallard1.png" alt="niklasallard1" width="60" height="60">
</a> <a href="https://github.com/acrozi">
  <img src="https://github.com/acrozi.png" alt="acrozi" width="60" height="60">
</a><a href="https://github.com/jirjis77">
  <img src="https://github.com/jirjis77.png" alt="jirjis77" width="60" height="60">
</a><a href="https://github.com/Heithum123">
  <img src="https://github.com/Heithum123.png" alt="Heithum123" width="60" height="60">
</a>

<<<<<<< HEAD
heithum är här
heithumm
=======
>>>>>>> 99d6b6c3b045f9fcd2f3f91749fd70826fc7a9e3
