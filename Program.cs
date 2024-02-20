using System;
using System.Collections.Generic;

class Program
{
    // Define a class to represent a to-do item
    class TodoItem
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime DueDate { get; set; }   
        public bool IsUrgent { get; set; }
        public bool IsDone { get; set; }
    }

    // Create a list to store to-do items
    static List<TodoItem> todoList = new List<TodoItem>();

    static void Main()
    {
        // Display a welcome message or menu
        Console.WriteLine("Welcome to the Command Line ToDo List!");

        // Run the program loop
        while (true)
        {
            // Display menu options
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. View ToDo List");
            Console.WriteLine("2. Add New Item");
            Console.WriteLine("3. Edit Item");
            Console.WriteLine("4. Delete Item");
            Console.WriteLine("5. Exit");

            // Get user input
            Console.Write("Enter your choice (1-5): ");
            string choice = Console.ReadLine();

            // Process user input
            switch (choice)
            {
                case "1":
                    ViewTodoList();
                    break;

                case "2":
                    AddNewItem();
                    break;

                case "3":
                    EditItem();
                    break;

                case "4":
                    DeleteItem();
                    break;

                case "5":
                    // Exit the program
                    Console.WriteLine("Exiting ToDo List. Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                    break;
            }
        }
    }

    // Method to display the current to-do list
    static void ViewTodoList()
    {
        // Check if the list is empty
        if (todoList.Count == 0)
        {
            Console.WriteLine("ToDo List is empty.");
        }
        else
        {
            Console.WriteLine("\nToDo List:");
            // Iterate through the list and display each item
            foreach (var item in todoList)
            {
                Console.WriteLine($"Title: {item.Title}");
                Console.WriteLine($"Body: {item.Body}");
                Console.WriteLine($"Due Date: {item.DueDate}");
                Console.WriteLine($"Urgent: {item.IsUrgent}");
                Console.WriteLine($"Status: {(item.IsDone ? "Completed" : "Pending")}");
                Console.WriteLine("----------------------------");
            }
        }
    }

    // Method to add a new item to the to-do list
    static void AddNewItem()
    {
        // Prompt the user for input to create a new to-do item
        Console.Write("Enter Title: ");
        string title = Console.ReadLine();

        Console.Write("Enter Body: ");
        string body = Console.ReadLine();

        DateTime dueDate;
        do
        {
            Console.Write("Enter Due Date (YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out dueDate))
            {
                Console.WriteLine("Invalid date format. Please try again.");
            }
            else if (dueDate < DateTime.UtcNow)
            {
                Console.WriteLine("Due date must be in the future. Please try again.");
            }
            else
            {
                break; //valid date
            }
        } while (true);
        
        
        Console.Write("Is it Urgent? (true/false): ");
        bool isUrgent = bool.Parse(Console.ReadLine());

        // Create a new to-do item and add it to the list
        TodoItem newItem = new TodoItem
        {
            Title = title,
            Body = body,
            DueDate = dueDate,
            IsUrgent = isUrgent,
            IsDone = false // By default, a new item is not done
        };

        todoList.Add(newItem);

        Console.WriteLine("New item added to the ToDo List.");
    }

    // Method to edit the attributes of an existing item
    static void EditItem()
    {
        // Prompt the user for the title of the item to edit
        Console.Write("Enter the Title of the item to edit: ");
        string titleToEdit = Console.ReadLine();

        // Find the item in the list based on the title
        TodoItem itemToEdit = todoList.Find(item => item.Title.Equals(titleToEdit, StringComparison.OrdinalIgnoreCase));

        if (itemToEdit != null)
        {
            // Display the current attributes of the item
            Console.WriteLine($"Editing item: {itemToEdit.Title}");
            Console.WriteLine($"Current Body: {itemToEdit.Body}");
            Console.WriteLine($"Current Due Date: {itemToEdit.DueDate}");
            Console.WriteLine($"Current Urgent status: {itemToEdit.IsUrgent}");
            Console.WriteLine($"Current Status: {(itemToEdit.IsDone ? "Completed" : "Pending")}");

            // Prompt the user for updated values
            Console.Write("Enter new Body (press Enter to keep current): ");
            string newBody = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newBody))
            {
                itemToEdit.Body = newBody;
            }

            Console.Write("Enter new Due Date (YYYY-MM-DD, press Enter to keep current): ");
            string newDueDateInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newDueDateInput))
            {
                itemToEdit.DueDate = DateTime.Parse(newDueDateInput);
            }

            Console.Write("Update Urgent status? (true/false, press Enter to keep current): ");
            string newUrgentInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newUrgentInput))
            {
                itemToEdit.IsUrgent = bool.Parse(newUrgentInput);
            }

            Console.Write("Update Status? (true for Completed/false for Not Completed, press Enter to keep current): ");
            string newStatus = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newStatus)) 
            {
                itemToEdit.IsDone = bool.Parse(newStatus);
            }

            // Display a confirmation message
            Console.WriteLine("Item updated successfully.");
        }
        else
        {
            Console.WriteLine($"Item with title '{titleToEdit}' not found in the ToDo List.");
        }
    }

    // Method to delete an item from the to-do list
    static void DeleteItem()
    {
        // Prompt the user for the title of the item to delete
        Console.Write("Enter the Title of the item to delete: ");
        string titleToDelete = Console.ReadLine();

        // Find the item in the list based on the title
        TodoItem itemToDelete = todoList.Find(item => item.Title.Equals(titleToDelete, StringComparison.OrdinalIgnoreCase));

        if (itemToDelete != null)
        {
            // Remove the item from the list
            todoList.Remove(itemToDelete);

            // Display a confirmation message
            Console.WriteLine($"Item '{itemToDelete.Title}' deleted from the ToDo List.");
        }
        else
        {
            Console.WriteLine($"Item with title '{titleToDelete}' not found in the ToDo List.");
        }
    }
}

