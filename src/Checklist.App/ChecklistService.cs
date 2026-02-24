namespace Checklist.App;

public class ChecklistService
{
    private readonly List<ChecklistItem> _items = new();
    private int _nextId = 1;

    public ChecklistItem AddItem(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        var item = new ChecklistItem(_nextId++, title.Trim());
        _items.Add(item);
        return item;
    }

    public IReadOnlyList<ChecklistItem> GetItems() => _items.AsReadOnly();

    public bool CompleteItem(int id)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item is null) return false;

        item.IsCompleted = true;
        return true;
    }

    public bool UncompleteItem(int id)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item is null) return false;

        item.IsCompleted = false;
        return true;
    }

    public bool RemoveItem(int id)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item is null) return false;

        _items.Remove(item);
        return true;
    }
}
