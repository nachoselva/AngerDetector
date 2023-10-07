# AngerDetector

This system allow the user to send an email and, based on the speed of the key strokes, it detects if the user is angry. 
In case it is, he recommends to take a break and calm down before sending the email.

## Tech Stack

Net Core 6
WPF
XUnit
Moq
FlaUI
Github Actions

## Testing strategy

It is being used XUnit, Moq and FlaUI. Tests are divided in automation and unit tests.

## Run Building

In order to build the project, run the following command in powershell:

´´´
dotnet restore | dotnet build AngerDetector.Application
´´´

## Run Testing

In order to run the tests suite, run the following command in powershell:

´´´
dotnet restore | dotnet build AngerDetector.Application | dotnet test AngerDetector.Tests
´´´

## Local enviroment

The project is prepared to be run in Visual Studio 2022, it does not require any further installations.

## Continuos Integration

Each time a push or pull request is merged to master, github automatically checkout the code, build the app and run the tests. 
In case this application is sent to production, we can add those steps in the current workflow.

## Known issues

Github repository configuration is pretty poor, as the repository is private, I was not able to configure branch protections and checks. In case I would be able to, I would like to do a previous check on builds and tests instead of doing it only once it is merged.
When the Test is project is running locally, once all the tests have been executed, all the tests are run again without user intervention. I'm investigating if this could be bug in this system or in FlaUI nuget package.

