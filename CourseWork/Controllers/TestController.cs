using CourseWork.Extensions;
using CourseWork.Models;
using CourseWork.Services;
using CourseWork.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers;

[Authorize]
public class TestController : Controller
{
    private readonly ITestService _testService;
    private readonly IAnswerService _answerService;
    private readonly IQuestionService _questionService;
    private readonly IUserResultService _userResultService;
    private readonly UserManager<User> _userManager;
    
    public TestController(ITestService testService, IAnswerService answerService, 
        UserManager<User> userManager, IQuestionService questionService, IUserResultService userResultService)
    {
        _testService = testService;
        _answerService = answerService;
        _userManager = userManager;
        _questionService = questionService;
        _userResultService = userResultService;
    }

    public IActionResult Index()
    {
        var tests = _testService.Tests.OrderByDescending(t => t.OpenTime).ToList();
        var sorted = tests.Where(t => DateTime.Now < t.CloseTime).OrderBy(t => t.OpenTime).ToList();
        sorted.AddRange(tests.Where(t => DateTime.Now > t.CloseTime).OrderByDescending(t => t.OpenTime).ToList());
        return View(sorted);
    }

    public async Task<IActionResult> TestInfo(Guid? testId) => View(await _testService.GetByIdAsync(testId));

    public async Task<IActionResult> Testing(Guid testId, QuestionViewModel model, int currentPage = 1)
    {
        var test = await _testService.GetByIdAsync(testId);
        if (test == null)
        {
            return RedirectToAction("Index", "Home");
        }
        
        if (DateTime.Now < test.OpenTime || DateTime.Now > test.CloseTime)
        {
            TempData["error-message"] = "В доступе отказано";
            return RedirectToAction("TestInfo", new {testId = test.Id});
        }

        if (model.UserAnswers.Any() && model.Answers.Any())
        {
            if (model.SelectedUserAnswerId != null)
            {
                model.UserAnswers
                    .Where(a => a.Id == model.SelectedUserAnswerId)
                    .ToList().ForEach(a => a.IsCorrect = true);
            }
            UpdateUserResult(model.UserAnswers, model.Answers, test, model.Question.Cost);
        }

        const int pageSize = 1;
        var questions = GetQuestions(test);
        var question = questions.Skip((currentPage - 1) * pageSize).Take(pageSize).FirstOrDefault();
        if (question == null)
        {
            return RedirectToAction("Error", "Home");
        }
        var answers = _answerService.Answers.Where(a => a.QuestionId == question.Id).ToList();
        var userAnswers = new List<UserAnswer>();
        
        for (var i = 0; i < answers.Count; i++)
        {
            userAnswers = userAnswers.Append(new UserAnswer
            {
                QuestionNumber = question.Number
            }).ToList();
        }
        
        return View(new QuestionViewModel
        {
            Question = question, 
            UserAnswers = userAnswers,
            Answers = answers,
            TestCloseTime = test.CloseTime,
            PageViewModel = new PageViewModel(test.QuestionsLimit, currentPage, pageSize)
        });
    }

    public void UpdateUserResult(List<UserAnswer> userAnswers, List<Answer> correctAnswers, Test test, int cost)
    {
        var userId = _userManager.GetUserId(HttpContext.User);
        var userResult = GetUserResult(test.Id, userId, test.QuestionsLimit);
        userResult.AddAnswers(userAnswers, correctAnswers, cost);
        SaveUserResult(userResult);
    }

    public async Task<IActionResult> EndTesting(QuestionViewModel model)
    {
        var test = await _testService.GetByIdAsync(model.Question.TestId);
        if (test == null)
        {
            return RedirectToAction("Index", "Home");
        }

        UpdateUserResult(model.UserAnswers, model.Answers, test, model.Question.Cost);

        var userId = _userManager.GetUserId(HttpContext.User);
        var userResult = GetUserResult(model.Question.TestId, userId, test.QuestionsLimit);
        await _userResultService.AddAsync(userResult);
        HttpContext.Session.Clear();
        ViewData["userResultId"] = userResult.Id;
        return View(userResult);
    }

    [HttpPost]
    public IActionResult BackToTests(Guid userResultId, string? comment)
    {
        if (string.IsNullOrEmpty(comment?.Trim())) 
            return RedirectToAction("Index");
        
        var userResult = _userResultService.UserResults.FirstOrDefault(r => r.Id == userResultId);
        if (userResult != null)
        {
            userResult.UserComment = comment;
            _userResultService.UpdateAsync(userResult);
        }
        return RedirectToAction("Index");
    }
    
    private IEnumerable<Question> GetQuestions(Test test)
    {
        var questions = HttpContext.Session.GetJson<IEnumerable<Question>>("Questions");
        if (questions != null) return questions;

        questions = _questionService.Questions.Where(q => q.TestId == test.Id).OrderBy(q => q.Number).ToList();

        if (test.ShuffleQuestions)
        {
            var random = new Random();
            questions = questions.OrderBy(_ => random.Next()).ToList();
        }
        
        SaveQuestions(questions);
        return questions;
    }

    private void SaveQuestions(IEnumerable<Question> questions)
    {
        HttpContext.Session.SetJson("Questions", questions);
    }

    private UserResult GetUserResult(Guid testId, string userId, int totalQuestions)
    {
        var userResult = HttpContext.Session.GetJson<UserResult>("UserResult");
        if (userResult != null) return userResult;
        
        userResult = new UserResult
        {
            TestId = testId,
            UserId = userId,
            TotalQuestions = totalQuestions
        };

        return userResult;
    }
    
    private void SaveUserResult(UserResult userResult)
    {
        HttpContext.Session.SetJson("UserResult", userResult);
    }
}