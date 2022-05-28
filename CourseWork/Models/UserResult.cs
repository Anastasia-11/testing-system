using System.ComponentModel.DataAnnotations;

namespace CourseWork.Models;

public class UserResult
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool TestIsCompleted { get; set; }
    public DateTime BeginTime { get; set; } = DateTime.Now;
    public DateTime EndTime { get; set; }
    public int CompletedQuestions { get; set; }
    public int RightQuestions { get; set; }
    public int Score { get; set; }
    public double RightPercent { get; set; }
    public int TotalQuestions { get; set; }
    public string? UserComment { get; set; }
    
    public string UserId { get; set; }
    public User? User { get; set; }
    
    public Guid TestId { get; set; }
    public Test? Test { get; set; }
    
    public IEnumerable<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
    
    public void AddAnswers(List<UserAnswer> currentUserAnswers, List<Answer> correctAnswers, int score)
    {
        currentUserAnswers = currentUserAnswers.OrderBy(a => a.AnswerNumber).ToList();
        if (UserAnswers.Any(a => a.QuestionNumber == currentUserAnswers[0].QuestionNumber))
        {
            var previousAnswers = UserAnswers.Where(a => a.QuestionNumber == currentUserAnswers[0].QuestionNumber)
                                                    .OrderBy(a => a.AnswerNumber).ToList();
            if (UserAnswerIsCorrect(previousAnswers, correctAnswers))
            {
                Score -= score;
                RightQuestions--;
            }
            for (int i = 0; i < currentUserAnswers.Count; i++)
            {
                previousAnswers[i].AnswerTime = DateTime.Now;
                previousAnswers[i].AnswerText = currentUserAnswers[i].AnswerText;
                previousAnswers[i].IsCorrect = currentUserAnswers[i].IsCorrect;
            }
        }
        else
        {
            foreach (var userAnswer in currentUserAnswers)
            {
                userAnswer.UserResultId = Id;
                userAnswer.AnswerTime = DateTime.Now;
                userAnswer.IsAnswered = true;
                UserAnswers = UserAnswers.Append(userAnswer);
            }
            CompletedQuestions++;
        }
        
        if (UserAnswerIsCorrect(currentUserAnswers, correctAnswers))
        {
            Score += score;
            RightQuestions++;
        }
        
        if (CompletedQuestions == TotalQuestions)
        {
            TestIsCompleted = true;
        }
        
        EndTime = DateTime.Now;
        RightPercent = (double)RightQuestions / TotalQuestions;
    }
    
    private bool UserAnswerIsCorrect(List<UserAnswer> userAnswers, List<Answer> correctAnswers)
    {
        if (userAnswers.Count == 1)
        {
            return userAnswers[0].AnswerText == correctAnswers[0].CorrectResponse;
        }

        for (var i = 0; i < userAnswers.Count; i++)
        {
            if (userAnswers[i].IsCorrect != correctAnswers[i].IsCorrect)
                return false;
        }
        
        return true;
    }
}