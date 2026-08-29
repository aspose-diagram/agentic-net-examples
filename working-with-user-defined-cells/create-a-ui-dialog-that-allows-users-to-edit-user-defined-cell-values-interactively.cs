using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Prompt for the Visio file to load
        Console.Write("Enter the path of the Visio file to load: ");
        string inputPath = Console.ReadLine();

        // Load the diagram
        Diagram diagram = new Diagram(inputPath);

        bool continueEditing = true;
        while (continueEditing)
        {
            // List all shapes with their IDs and page indices
            Console.WriteLine("\nAvailable shapes:");
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}, Name: {shape.NameU}");
                }
            }

            // Ask user to select a shape by ID
            Console.Write("\nEnter the Shape ID you want to edit (or 'exit' to finish): ");
            string shapeInput = Console.ReadLine();
            if (shapeInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            if (!long.TryParse(shapeInput, out long shapeId))
            {
                Console.WriteLine("Invalid Shape ID.");
                continue;
            }

            // Find the shape with the given ID
            Shape targetShape = FindShapeById(diagram, shapeId);
            if (targetShape == null)
            {
                Console.WriteLine("Shape not found.");
                continue;
            }

            // List user-defined cells for the selected shape
            if (targetShape.Users.Count == 0)
            {
                Console.WriteLine("No user-defined cells found for this shape.");
                continue;
            }

            Console.WriteLine("\nUser-defined cells:");
            for (int i = 0; i < targetShape.Users.Count; i++)
            {
                User userCell = targetShape.Users[i];
                Console.WriteLine($"{i}: Name = {userCell.Name}, Value = {userCell.Value.Val}");
            }

            // Ask which cell to edit
            Console.Write("\nEnter the index of the cell to edit: ");
            string cellIndexInput = Console.ReadLine();
            if (!int.TryParse(cellIndexInput, out int cellIndex) ||
                cellIndex < 0 || cellIndex >= targetShape.Users.Count)
            {
                Console.WriteLine("Invalid cell index.");
                continue;
            }

            User selectedUserCell = targetShape.Users[cellIndex];
            Console.Write($"Current value of '{selectedUserCell.Name}' is '{selectedUserCell.Value.Val}'. Enter new value: ");
            string newValue = Console.ReadLine();

            // Update the cell value
            selectedUserCell.Value.Val = newValue;
            Console.WriteLine("Value updated.");

            // Ask if the user wants to edit another cell/shape
            Console.Write("\nEdit another cell? (y/n): ");
            string answer = Console.ReadLine();
            continueEditing = answer.Equals("y", StringComparison.OrdinalIgnoreCase);
        }

        // Prompt for output path and save the diagram
        Console.Write("\nEnter the output file path (e.g., output.vsdx): ");
        string outputPath = Console.ReadLine();
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
        Console.WriteLine("Diagram saved successfully.");
    }

    // Helper method to locate a shape by its ID across all pages
    static Shape FindShapeById(Diagram diagram, long shapeId)
    {
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                if (shape.ID == shapeId)
                {
                    return shape;
                }
            }
        }
        return null;
    }
}
