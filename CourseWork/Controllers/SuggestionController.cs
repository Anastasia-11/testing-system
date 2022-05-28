using CourseWork.Models;
using CourseWork.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers;

public class SuggestionController : Controller
{
    private readonly ISuggestionService _suggestionService;

    public SuggestionController(ISuggestionService suggestionService)
    {
        _suggestionService = suggestionService;
    }

    public IActionResult Suggestions() => View(_suggestionService.Suggestions);
    
    public IActionResult CreateSuggestion()
    {
        return View(new Suggestion());
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateSuggestion(Suggestion suggestion, IFormFile? image = null)
    {
        if (!ModelState.IsValid)
            return View(suggestion);

        if (image != null)
        {
            using (var binaryReader = new BinaryReader(image.OpenReadStream()))
            {
                suggestion.ImageData = binaryReader.ReadBytes((int)image.Length);
            }
        }
        
        await _suggestionService.AddAsync(suggestion);
        return RedirectToAction("Index", "Home");
    }

    public IActionResult SuggestionInfo(Guid suggestionId)
    {
        var suggestion = _suggestionService.GetByIdAsync(suggestionId).Result;
        return View(suggestion);
    }
    
    public async Task<IActionResult> DeleteSuggestion(Guid? suggestionId)
    {
        var suggestion = _suggestionService.GetByIdAsync(suggestionId).Result;
        if (suggestion != null)
        {
            await _suggestionService.DeleteAsync(suggestionId);
        }
        
        return RedirectToAction("Suggestions");
    }
}