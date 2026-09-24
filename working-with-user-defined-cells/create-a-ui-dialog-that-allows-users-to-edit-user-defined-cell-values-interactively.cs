using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Prompt for the Visio file to edit
        Console.Write("Enter the path of the Visio file to load (e.g., input.vsdx): ");
        string inputPath = Console.ReadLine();

        // Load the diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to load diagram: " + ex.Message);
            return;
        }

        bool continueEditing = true;
        while (continueEditing)
        {
            // List pages
            Console.WriteLine("\nPages in the diagram:");
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];
                Console.WriteLine($"{i + 1}. {page.Name}");
            }

            // Select a page
            Console.Write("Select a page number (or 0 to exit): ");
            if (!int.TryParse(Console.ReadLine(), out int pageChoice) || pageChoice < 0 || pageChoice > diagram.Pages.Count)
            {
                Console.WriteLine("Invalid selection.");
                continue;
            }
            if (pageChoice == 0)
            {
                break;
            }

            Page selectedPage = diagram.Pages[pageChoice - 1];

            // List shapes on the selected page
            Console.WriteLine($"\nShapes on page \"{selectedPage.Name}\":");
            foreach (Shape shape in selectedPage.Shapes)
            {
                // Shape.ID is a long identifier
                Console.WriteLine($"ID: {shape.ID}, Name: {shape.Name}");
            }

            // Select a shape
            Console.Write("Enter the ID of the shape you want to edit (or 0 to cancel): ");
            if (!long.TryParse(Console.ReadLine(), out long shapeId) || shapeId < 0)
            {
                Console.WriteLine("Invalid shape ID.");
                continue;
            }
            if (shapeId == 0)
            {
                continue;
            }

            Shape targetShape;
            try
            {
                targetShape = selectedPage.Shapes.GetShape(shapeId);
            }
            catch
            {
                Console.WriteLine("Shape not found.");
                continue;
            }

            // List user-defined cells for the shape
            if (targetShape.Users.Count == 0)
            {
                Console.WriteLine("This shape has no user-defined cells.");
                continue;
            }

            Console.WriteLine("\nUser-defined cells for the selected shape:");
            for (int i = 0; i < targetShape.Users.Count; i++)
            {
                User user = targetShape.Users[i];
                Console.WriteLine($"{i + 1}. Name: {user.Name}, Value: {user.Value.Val}, Prompt: {user.Prompt.Value}");
            }

            // Select a user-defined cell
            Console.Write("Select a cell number to edit (or 0 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int cellChoice) || cellChoice < 0 || cellChoice > targetShape.Users.Count)
            {
                Console.WriteLine("Invalid selection.");
                continue;
            }
            if (cellChoice == 0)
            {
                continue;
            }

            User selectedUser = targetShape.Users[cellChoice - 1];

            // Prompt for new value
            Console.Write($"Enter new value for cell \"{selectedUser.Name}\": ");
            string newValue = Console.ReadLine();

            // Update the cell value
            selectedUser.Value.Val = newValue;
            Console.WriteLine("Cell value updated.");

            // Ask whether to continue editing
            Console.Write("\nDo you want to edit another cell? (y/n): ");
            string response = Console.ReadLine();
            if (!string.Equals(response, "y", StringComparison.OrdinalIgnoreCase))
            {
                continueEditing = false;
            }
        }

        // Save the modified diagram
        Console.Write("\nEnter the path to save the modified diagram (e.g., output.vsdx): ");
        string outputPath = Console.ReadLine();

        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to save diagram: " + ex.Message);
        }
    }
}
