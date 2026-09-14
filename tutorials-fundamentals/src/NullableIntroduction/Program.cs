// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/nullable-reference-types

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using NullableIntroduction;

/*
dotnet Program.cs

I'm using
    dotnet run --project src/NullableIntroduction
to build and run Program.csproj.
This loads its NuGet package references, including System.CommandLine.
*/

Console.WriteLine("Hello, NullableIntroduction!");

var surveyRun = new SurveyRun();
surveyRun.AddQuestion(QuestionType.YesNo, "Has your code ever thrown a NullReferenceException?");
surveyRun.AddQuestion(new SurveyQuestion(QuestionType.Number, "How many times (to the nearest 100) has that happened?"));
surveyRun.AddQuestion(QuestionType.Text, "What is your favorite color?");

// compilation error: cannot convert from 'string' to 'NullableIntroduction.QuestionType'
// surveyRun.AddQuestion(QuestionType.Text, default);

surveyRun.PerformSurvey(50);

foreach (var participant in surveyRun.AllParticipants)
{
    Console.WriteLine($"Participant: {participant.Id}:");
    if (participant.AnsweredSurvey)
    {
        for (int i = 0; i < surveyRun.Questions.Count; i++)
        {
            var answer = participant.Answer(i);
            Console.WriteLine($"\t{surveyRun.GetQuestion(i).QuestionText} : {answer}");
        }
    }
    else
    {
        Console.WriteLine("\tNo responses");
    }
}

/* Examine the survey results
https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/nullable-reference-types#examine-the-survey-results
Notice that no null check is needed for participant, surveyRun.Questions, or surveyRun.GetQuestion(i). 
The types declare those values as non-nullable, so the compiler treats them as not-null throughout the loop.
*/