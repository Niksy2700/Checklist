using Checklist.App;

var service = new ChecklistService();
bool running = true;

Console.WriteLine("=== Checklist App ===");

while (running)
{
    Console.WriteLine();
    Console.WriteLine("Commands: add, list, complete, uncomplete, remove, quit");
    Console.Write("> ");
    string? input = Console.ReadLine()?.Trim().ToLower();

    switch (input)
    {
        case "add":
            Console.Write("Enter item title: ");
            string? title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                var item = service.AddItem(title);
                Console.WriteLine($"Added: {item}");
            }
            else
            {
                Console.WriteLine("Title cannot be empty.");
            }
            break;

        case "list":
            var items = service.GetItems();
            if (items.Count == 0)
            {
                Console.WriteLine("No items in the checklist.");
            }
            else
            {
                foreach (var i in items)
                    Console.WriteLine(i);
            }
            break;

        case "complete":
            Console.Write("Enter item ID to mark as complete: ");
            if (int.TryParse(Console.ReadLine(), out int completeId))
            {
                if (service.CompleteItem(completeId))
                    Console.WriteLine("Item marked as complete.");
                else
                    Console.WriteLine($"Item {completeId} not found.");
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
            break;

        case "uncomplete":
            Console.Write("Enter item ID to mark as incomplete: ");
            if (int.TryParse(Console.ReadLine(), out int uncompleteId))
            {
                if (service.UncompleteItem(uncompleteId))
                    Console.WriteLine("Item marked as incomplete.");
                else
                    Console.WriteLine($"Item {uncompleteId} not found.");
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
            break;

        case "remove":
            Console.Write("Enter item ID to remove: ");
            if (int.TryParse(Console.ReadLine(), out int removeId))
            {
                if (service.RemoveItem(removeId))
                    Console.WriteLine("Item removed.");
                else
                    Console.WriteLine($"Item {removeId} not found.");
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
            break;

        case "quit":
        case "exit":
            running = false;
            break;

        default:
            Console.WriteLine("Unknown command. Use: add, list, complete, uncomplete, remove, quit");
            break;
    }
}

Console.WriteLine("Goodbye!");
