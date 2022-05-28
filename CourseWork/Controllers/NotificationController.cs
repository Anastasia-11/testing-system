using System.Net;
using System.Net.Mail;
using CourseWork.Constants;
using CourseWork.Models;
using CourseWork.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers;

public class NotificationController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly ITestService _testService;
    
    public NotificationController(UserManager<User> userManager, ITestService testService)
    {
        _userManager = userManager;
        _testService = testService;
    }

    public IActionResult ConfigureNotifications(Guid testId)
    {
        ViewData["testId"] = testId;
        var users = _userManager.GetUsersInRoleAsync("student").Result;
        users = users.OrderBy(u => u.UserName).ThenBy(u => u.Group).ToList();
        return View(users.ToList());
    }
    
    [HttpPost]
    public async Task<IActionResult> ConfigureNotifications(List<string> emails, Guid testId, string? comment)
    {
        var test = _testService.Tests.FirstOrDefault(t => t.Id == testId);
        if (test == null)
            return RedirectToAction("Index", "Test");
        
        foreach (var email in emails)
        {
            await SendEmail(email, comment, test);
        }
        return RedirectToAction("Index", "Test");
    }

    private async Task SendEmail(string email, string? comment, Test test)
    {
        var from = new MailAddress(NotificationConstants.EmailFrom, "Предстоящее тестирование");
        var to = new MailAddress(email);
        var message = new MailMessage(from, to);

        message.Subject = $"Тестирование по дисциплине \"{test.Category}\"";
        message.Body = GetMessage(test);
        if (!string.IsNullOrEmpty(comment?.Trim()))
        {
            message.Body += "<p>Комментарий преподавателя:\t" + comment +"<p>";
        }
        message.IsBodyHtml = true;
        
        var smtp = new SmtpClient(NotificationConstants.GmailHost, NotificationConstants.GmailPort);
        smtp.Credentials = new NetworkCredential(NotificationConstants.EmailFrom, NotificationConstants.Password);
        smtp.EnableSsl = true;
        await smtp.SendMailAsync(message);
    }

    private string GetMessage(Test test)
    {
        return "<h4>Информация о тестировании</h4>" +
               "<p>Вы записаны на прохождение тестирования по дисциплине \"" + 
               test.Category + "\". Это автоматическое уведомление от системы тестирования.</p>" +
               "<p><b>Время начала тестирования:</b>\t\t" + test.OpenTime + "</p>" +
               "<p><b>Время окончания тестирования:</b>\t" + test.CloseTime + "</p>" +
               "<p><b>Описание:</b>\t<p>" + test.Description + "</p> </p>" +
               "<p><b>Вопросов в тесте:</b>\t\t\t" + test.QuestionsLimit + "</p>" +
               "<p>Ссылка на тест: testingsystems.com/tests/testInfo/" + test.Id + "</p>";
    }
}