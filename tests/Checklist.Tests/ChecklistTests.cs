using Checklist.App;

namespace Checklist.Tests;

public class ChecklistServiceTests
{
    [Fact]
    public void AddItem_ValidTitle_ReturnsItemWithId()
    {
        var service = new ChecklistService();
        var item = service.AddItem("Buy milk");
        Assert.Equal(1, item.Id);
        Assert.Equal("Buy milk", item.Title);
        Assert.False(item.IsCompleted);
    }

    [Fact]
    public void AddItem_AssignsIncrementingIds()
    {
        var service = new ChecklistService();
        var first = service.AddItem("First");
        var second = service.AddItem("Second");
        Assert.Equal(1, first.Id);
        Assert.Equal(2, second.Id);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void AddItem_EmptyOrWhitespaceTitle_ThrowsArgumentException(string title)
    {
        var service = new ChecklistService();
        Assert.Throws<ArgumentException>(() => service.AddItem(title));
    }

    [Fact]
    public void GetItems_ReturnsAllAddedItems()
    {
        var service = new ChecklistService();
        service.AddItem("Task 1");
        service.AddItem("Task 2");
        var items = service.GetItems();
        Assert.Equal(2, items.Count);
    }

    [Fact]
    public void GetItems_EmptyByDefault()
    {
        var service = new ChecklistService();
        Assert.Empty(service.GetItems());
    }

    [Fact]
    public void CompleteItem_ExistingId_MarksAsCompleted()
    {
        var service = new ChecklistService();
        var item = service.AddItem("Buy milk");
        bool result = service.CompleteItem(item.Id);
        Assert.True(result);
        Assert.True(item.IsCompleted);
    }

    [Fact]
    public void CompleteItem_NonExistingId_ReturnsFalse()
    {
        var service = new ChecklistService();
        bool result = service.CompleteItem(999);
        Assert.False(result);
    }

    [Fact]
    public void UncompleteItem_CompletedItem_MarksAsIncomplete()
    {
        var service = new ChecklistService();
        var item = service.AddItem("Buy milk");
        service.CompleteItem(item.Id);
        bool result = service.UncompleteItem(item.Id);
        Assert.True(result);
        Assert.False(item.IsCompleted);
    }

    [Fact]
    public void UncompleteItem_NonExistingId_ReturnsFalse()
    {
        var service = new ChecklistService();
        bool result = service.UncompleteItem(999);
        Assert.False(result);
    }

    [Fact]
    public void RemoveItem_ExistingId_RemovesItem()
    {
        var service = new ChecklistService();
        var item = service.AddItem("Buy milk");
        bool result = service.RemoveItem(item.Id);
        Assert.True(result);
        Assert.Empty(service.GetItems());
    }

    [Fact]
    public void RemoveItem_NonExistingId_ReturnsFalse()
    {
        var service = new ChecklistService();
        bool result = service.RemoveItem(999);
        Assert.False(result);
    }
}

public class ChecklistItemTests
{
    [Fact]
    public void ToString_IncompleteItem_ShowsUnchecked()
    {
        var item = new ChecklistItem(1, "Buy milk");
        Assert.Equal("[ ] 1. Buy milk", item.ToString());
    }

    [Fact]
    public void ToString_CompletedItem_ShowsChecked()
    {
        var item = new ChecklistItem(1, "Buy milk");
        item.IsCompleted = true;
        Assert.Equal("[x] 1. Buy milk", item.ToString());
    }
}
