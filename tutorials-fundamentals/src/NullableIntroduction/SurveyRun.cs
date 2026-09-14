// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/nullable-reference-types#build-a-survey-of-questions

namespace NullableIntroduction;

public class SurveyRun
{
    private List<SurveyQuestion> surveyQuestions = [];

    public void AddQuestion(QuestionType type, string question) =>
        AddQuestion(new SurveyQuestion(type, question));

    public void AddQuestion(SurveyQuestion surveyQuestion) =>
        surveyQuestions.Add(surveyQuestion);

    /* https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/nullable-reference-types#examine-the-survey-results
    AllParticipants returns a non-nullable sequence even though respondents might be null.
    The ?? operator substitutes Enumerable.Empty<SurveyResponse>() when the field isn't populated yet. 
    If you remove the ?? clause, the compiler warns that the method might return null despite a non-nullable return type.
    */
    public IEnumerable<SurveyResponse> AllParticipants => (respondents ?? Enumerable.Empty<SurveyResponse>());

    public ICollection<SurveyQuestion> Questions => surveyQuestions;
    public SurveyQuestion GetQuestion(int index) => surveyQuestions[index];

    private List<SurveyResponse>? respondents;
    public void PerformSurvey(int numberOfRespondents)
    {
        int respondentsConsenting = 0;
        respondents = [];
        while (respondentsConsenting < numberOfRespondents)
        {
            var respondent = SurveyResponse.GetRandomId();
            if (respondent.AnswerSurvey(surveyQuestions))
                respondentsConsenting++;
            respondents.Add(respondent);
        }
    }

}