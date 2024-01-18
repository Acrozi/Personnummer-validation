<div align="center">

![Workflow build](https://github.com/Acrozi/Personnummer-validation/actions/workflows/docker.yml/badge.svg)
![Workflow test](https://github.com/Acrozi/Personnummer-validation/actions/workflows/tests.yml/badge.svg)

</div>

<div align="center">
  <h1>Personnummer Verification Console Application</h1>
</div>


## Introduktion

Det här projektet är en C#-baserad konsolapplikation för att verifiera svenska personnummer. 
Nedan finner du dokumentation om hur du kan köra och testa applikationen både lokalt och genom att använda Docker-container.

## Lokal Körning och Testning

För att köra och testa applikationen lokalt, följ stegen nedan:

1. Klona projektet från GitHub:
    bash
    `git clone https://github.com/Acrozi/Personnummer-validation.git`

2. Navigera till projektmappen:
    bash
    `cd Personnummer-validation`

3. Öppna projektet i din C#-utvecklingsmiljö t.ex., Visual Studio eller VS Code.

4. Bygg projektet:
    bash
    `dotnet build`

5. Kör applikationen:
    bash
    `dotnet run`

6. Använd applikationen för att verifiera personnummer:
    Följ instruktionerna som visas i konsolen för att ange ett svenskt personnummer och se resultatet av verifieringen.


7. Kör enhetstester:
    bash
    `dotnet test`

Se resultaten av enhetstesterna för att säkerställa korrekt funktionalitet.

## Körning med Docker

För att korrekt installera och köra programmet "Personnummer Validation" via Docker, följ dessa steg:

1. Se till att Docker är korrekt installerat och att Docker Desktop körs. Kontrollera att Docker Daemon är igång. (https://www.docker.com/products/docker-desktop/)

2. Använd kommandot `docker pull acrozi/personnummer_validation-docker` för att hämta Docker-bilden från Docker Hub och ladda ner den lokalt.

3. Använd kommandot `docker images` för att visa en lista över alla lokalt sparade Docker-images. Kontrollera att `acrozi/personnummer_validation-docker` finns i listan.

4. Använd kommandot `docker run acrozi/personnummer_validation-docker` för att köra programmet i en Docker-container. För Windows-användare som använder en terminal som stöder interaktivt läge kan du använda kommandot utan winpty. Om du har problem med det, prova med  kommandot `winpty docker run -it acrozi/personnummer_validation-docker`. Kommandot `-it` möjliggör att vi kan köra programmet interaktivt vilket gör att vi kan mata in data.

5. Följ instruktionerna i terminalen för att interagera med programmet. Ange personnummer när det efterfrågas.


## Körning med .EXE

Via github actions skapas ett körbart program.

1. Gå in på webbsidan https://github.com/Acrozi/Personnummer-validation/actions/runs/7572835865/artifacts/1178651670

2. Ladda ned och packa upp zip-filen och kör programmet.

3. Följ instruktionerna i terminalen för att interagera med programmet. Ange personnummer när det efterfrågas.

## Enhetstester

För att säkerställa korrekt funktionalitet har enhetstester skapats med hjälp av xUnit-ramverket. Några av de testade scenarierna inkluderar:

1. Validering av personnummer med korrekt format.

2. Ogiltiga personnummer i olika format som förväntas returnera false.

3. Hämtning av könet baserat på näst sista siffran i giltiga personnummer.

4. Kastning av ArgumentException vid försök att hämta kön från ett ogiltigt personnummer.

5. Projektstruktur:

Projektet innehåller två projekt

PersonNummerValidationTool: Huvudprojektet som innehåller kodfilen SwedishPersonalNumberValidator.cs.
xUnitTest: Projektet som innehåller xUnit-testerna för att validera koden

## Personnummer

Personnummer är ett nummer som Skatteverket tilldelar personer folkbokförda i Sverige för att identifiera dem hos bland annat myndigheter. Systemet, som var det första i världen som omfattade ett lands hela befolkning, infördes 1 januari 1947.

Ett personnummer är uppbyggt av 10 siffror indelade i två grupper om 6 respektive 4 siffror. Grupperna är åtskilda med ett skiljetecken, normalt ett bindestreck (-).

Ett personnummer kan matas in på olika sätt, bland annat genom 12 siffror, 10 siffror med bindestreck eller 10 siffror utan bindestreck.

För att kontrollera detta använder programmet funktionen `IsValid`:

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
Detta kontrollerar programmet genom funktionen `GetGender`:

    
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
Detta kontrollerar programmet genom funktionen `CalculateChecksum`:


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
## Svenska Regler för Personnummer

Svenska personnummer följer ett specifikt format och har regler för att bestämma dess giltighet. Applikationen genomför kontroller för att säkerställa att personnumret är korrekt enligt dessa regler. 

## Övrig Information

För ytterligare detaljer och användning, se dokumentationen i källkoden och README.md filen i GitHub.

Lycka till med användningen av personnummerkontroll applikationen!

<div align="center">
  <h2>Collaborators</h2>
  <a href="https://github.com/Acrozi/Personnummer-validation/graphs/contributors">
    <img src="https://contrib.rocks/image?repo=Acrozi/Personnummer-validation" />
  </a>
</div>
