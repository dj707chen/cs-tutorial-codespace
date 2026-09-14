// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/nullable-reference-types#build-the-survey-questions

namespace NullableIntroduction;

public enum QuestionType
{
    YesNo,
    Number,
    Text
}

public class SurveyQuestion(QuestionType typeOfQuestion, string text)
{
    public string QuestionText { get; } = text;
    public QuestionType TypeOfQuestion { get; } = typeOfQuestion;
}