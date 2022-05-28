using CourseWork.Models;
using CourseWork.Services;
using CourseWork.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers;

public class StatisticsController : Controller
{
    private readonly IUserResultService _userResultService;
    private readonly IUserAnswerService _userAnswerService;
    private readonly IAnswerService _answerService;
    private readonly ITestService _testService;
    private readonly IQuestionService _questionService;
    private readonly UserManager<User> _userManager;

    public StatisticsController(IUserResultService userResultService, UserManager<User> userManager, ITestService testService, IUserAnswerService userAnswerService, IQuestionService questionService, IAnswerService answerService)
    {
        _userResultService = userResultService;
        _userManager = userManager;
        _testService = testService;
        _userAnswerService = userAnswerService;
        _questionService = questionService;
        _answerService = answerService;
    }

    public IActionResult UserStatistics(Guid? userId = null)
    {
        var id = userId == null ? _userManager.GetUserId(HttpContext.User) : userId.ToString();
        var userResults = _userResultService.UserResults.Where(t => t.UserId == id).OrderBy(r => r.RightPercent).ToList();

        foreach (var userResult in userResults)
        {
            userResult.Test = _testService.Tests.FirstOrDefault(t => t.Id == userResult.TestId);
        }
        return View(userResults);
    }

    public IActionResult UserResultInfo(Guid userResultId)
    {
        return View(GetUserResultInfoModel(userResultId));
    }
    
    public IActionResult UserResultInfoWithAnswers(Guid userResultId)
    {
        if (!User.IsInRole("tutor") && !User.IsInRole("admin"))
        {
            return RedirectToAction("Index", "Test");
        }
        return View(GetUserResultInfoModel(userResultId));
    }

    private UserResultViewModel GetUserResultInfoModel(Guid userResultId)
    {
        var userResult = _userResultService.UserResults.First(r => r.Id == userResultId);
        userResult.Test = _testService.Tests.FirstOrDefault(t => t.Id == userResult.TestId);
        userResult.UserAnswers = _userAnswerService.UserAnswers.Where(a => a.UserResultId == userResult.Id).OrderBy(a => a.QuestionNumber).ThenBy(a => a.AnswerNumber).ToList();
        var questions = _questionService.Questions.Where(q => q.TestId == userResult.TestId).OrderBy(q => q.Number).ToList();

        var model = new UserResultViewModel
        {
            UserResult = userResult,
            Questions = new List<Question>()
        };
        
        foreach (var question in questions)
        {
            foreach (var userAnswer in userResult.UserAnswers)
            {
                if (userAnswer.QuestionNumber == question.Number)
                {
                    model.Questions.Add(question);
                    model.Questions[^1].Answers = new List<Answer>(_answerService.Answers.Where(a => a.QuestionId == question.Id)).OrderBy(a => a.Number);
                    break;
                }
            }
        }
        
        return model;
    }

    public IActionResult UsersStatistics()
    {
        var users = _userManager.GetUsersInRoleAsync("student").Result;
        users = users.OrderBy(u => u.UserName).ThenBy(u => u.Group).ToList();
        return View(users.ToList());
    }

    public IActionResult TestStatistics(Guid? testId = null)
    {
        var userResults = _userResultService.UserResults.Where(r => r.TestId == testId).ToList();
        foreach (var userResult in userResults)
        {
            userResult.User = _userManager.Users.FirstOrDefault(u => u.Id == userResult.UserId);
        }
        return View(userResults);
    }
}