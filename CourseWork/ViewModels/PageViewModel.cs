namespace CourseWork.ViewModels;

public class PageViewModel
{
    public int CurrentPageNumber { get; }
    public int TotalPages { get; }

    public PageViewModel(int count, int currentPageNumber, int pageSize)
    {
        CurrentPageNumber = currentPageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }

    public bool HasPreviousPage => CurrentPageNumber > 1;

    public bool HasNextPage => CurrentPageNumber < TotalPages;
}