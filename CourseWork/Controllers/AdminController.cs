using System.Transactions;
using CourseWork.Models;
using CourseWork.Services;
using CourseWork.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers;

[Authorize(Roles="admin, tutor")]
public class AdminController : Controller
{
    private readonly ITestService _testService;
    private readonly IQuestionService _questionService;
    private readonly IAnswerService _answerService;
    
    public AdminController(ITestService testService, IQuestionService questionService, IAnswerService answerService)
    {
        _testService = testService;
        _questionService = questionService;
        _answerService = answerService;
    }
    
    public IActionResult Tests() => View(_testService.Tests);

    [HttpGet]
    public IActionResult CreateTest()
    {
        return View(new CreateTestViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> CreateTest(CreateTestViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        
        var test = new Test 
        { 
            Id = model.TestId,
            Category = model.Category, Name = model.Name, 
            Description = model.Description, OpenTime = model.OpenTime, 
            CloseTime = model.CloseTime, QuestionsCount = model.Questions.Count,
            QuestionsLimit = model.QuestionsLimit,
            ShuffleQuestions = model.ShuffleQuestions,
            Questions = model.Questions.AsEnumerable()
        };

        await _testService.AddAsync(test);
        return RedirectToAction("Tests");
    }
    
    [HttpGet]
    public async Task<IActionResult> EditTest(Guid? testId)
    {
        if (testId == null) return NotFound();
        var test = await _testService.GetByIdAsync(testId);
        if (test == null) return NotFound();
        var model = new EditTestViewModel
        {
            TestId = test.Id,
            Category = test.Category, Name = test.Name,
            Description = test.Description,
            OpenTime = test.OpenTime, CloseTime = test.CloseTime,
            QuestionsLimit = test.QuestionsLimit,
            QuestionsCount = test.QuestionsCount,
            ShuffleQuestions = test.ShuffleQuestions,
            Questions = _questionService.Questions.Where(q => q.TestId == test.Id).OrderBy(q => q.Number).ToList(),
        };
        
        foreach (var question in model.Questions)
        {
            question.Answers = _answerService.Answers.Where(a => a.QuestionId == question.Id).OrderBy(a => a.Number).ToList();
        }
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> EditTest(EditTestViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var test = await _testService.GetByIdAsync(model.TestId);
        if (test == null) return View(model);
        
        using (var scope = new TransactionScope(
                   TransactionScopeAsyncFlowOption.Enabled))
        {
            await _testService.DeleteAsync(test.Id).ConfigureAwait(false);
            scope.Complete();
        }
        
        test.Category = model.Category;
        test.Name = model.Name;
        test.Description = model.Description;
        test.OpenTime = model.OpenTime;
        test.CloseTime = model.CloseTime;
        test.QuestionsLimit = model.QuestionsLimit;
        test.QuestionsCount = model.Questions.Count;
        test.Questions = model.Questions.AsEnumerable();
        test.ShuffleQuestions = model.ShuffleQuestions;

        using (var scope = new TransactionScope(
                   TransactionScopeAsyncFlowOption.Enabled))
        {
            await _testService.AddAsync(test).ConfigureAwait(false);
            scope.Complete();
        }

        return RedirectToAction("Tests");
    }
    
    public async Task<IActionResult> DeleteTest(Guid? testId)
    {
        if (testId == null) return NotFound();
        await _testService.DeleteAsync(testId);
        return RedirectToAction("Tests");
    }
}