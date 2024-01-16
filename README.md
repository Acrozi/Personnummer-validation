# To-do

# Personnummer Verification Console Application

## Introduktion

Det här projektet är en C#-baserad konsolapplikation för att verifiera svenska personnummer. 
Nedan finner du dokumentation om hur du kan köra och testa applikationen både lokalt och genom att använda Docker-container.

## Lokal Körning och Testning

För att köra och testa applikationen lokalt, följ stegen nedan:

1. **Klona projektet från GitHub:**
    bash
    git clone https://github.com/Acrozi/Personnummer-validation.git












###########################################################################################

Niklas del

## Personnummer

Personnummer är ett nummer som skatteverket tilldelar personer folkbokförda i Sverige för att identifiera dem hos bland annat myndigheter.
Systemet, som var det första i världen som omfattade ett lands hela befolkning, infördes 1 januari 1947.

Ett personnummer är uppbyggt av 10 siffror indelade i två grupper om 6 respektive 4 siffror. 
Grupperna är åtskiljda med ett skiljetecken, normalt ett bindestreck (-).

Ett personnummer kan matas in på olika sätt, bland annat genom 12 siffror, 10 siffror med bindestreck eller 10 siffror utan bindestreck.

För att kontrollera detta använder vi funktionen IsValid:

//

public static bool IsValid(string personalNumber)
    {
        // Ta bort bindestreck om de finns
        personalNumber = personalNumber.Replace("-", "");

       // Konvertera 12-siffrigt format till 10-siffrigt format
        if (personalNumber.Length == 12)
        {
            personalNumber = personalNumber.Substring(2, 10);
        } 

