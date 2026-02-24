namespace Checklist.App;

public class ChecklistItem
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public bool IsCompleted { get; set; }

    public ChecklistItem(int id, string title)
    {
        Id = id;
        Title = title;
        IsCompleted = false;
    }

    public override string ToString()
    {
        string status = IsCompleted ? "[x]" : "[ ]";
        return $"{status} {Id}. {Title}";
    }
}
