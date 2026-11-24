using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Prompt user for the Visio diagram file path
        Console.Write("Enter the path to the Visio diagram file: ");
        string inputPath = Console.ReadLine();

        // Guard: ensure the file exists
        if (string.IsNullOrWhiteSpace(inputPath) || !File.Exists(inputPath))
        {
            Console.Error.WriteLine("Error: The specified file does not exist.");
            return;
        }

        Diagram diagram;
        try
        {
            // Load the diagram
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        bool continueEditing = true;
        while (continueEditing)
        {
            // List all pages and shapes with their IDs
            Console.WriteLine("\nAvailable Shapes:");
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    Console.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}, Shape Name: {shape.Name}");
                }
            }

            // Prompt for shape ID
            Console.Write("\nEnter the Shape ID you want to edit (or press Enter to finish): ");
            string shapeIdInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(shapeIdInput))
            {
                // User chose to finish editing
                break;
            }

            if (!long.TryParse(shapeIdInput, out long targetShapeId))
            {
                Console.Error.WriteLine("Invalid Shape ID. Please enter a numeric value.");
                continue;
            }

            // Locate the shape across all pages
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.ID == targetShapeId)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null) break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("Shape not found. Please verify the Shape ID.");
                continue;
            }

            // List user-defined cells for the selected shape
            if (targetShape.Users.Count == 0)
            {
                Console.WriteLine("The selected shape has no user-defined cells.");
                continue;
            }

            Console.WriteLine("\nUser-defined Cells:");
            int index = 1;
            foreach (User userCell in targetShape.Users)
            {
                Console.WriteLine($"{index}. NameU: {userCell.NameU}, Value: {userCell.Value.Val}, Prompt: {userCell.Prompt.Value}");
                index++;
            }

            // Prompt for which cell to edit
            Console.Write("\nEnter the number of the cell you want to edit (or press Enter to skip): ");
            string cellIndexInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(cellIndexInput))
            {
                // Skip editing this shape
                continue;
            }

            if (!int.TryParse(cellIndexInput, out int cellNumber) || cellNumber < 1 || cellNumber > targetShape.Users.Count)
            {
                Console.Error.WriteLine("Invalid cell number.");
                continue;
            }

            // Retrieve the selected User object
            User selectedUser = targetShape.Users[cellNumber - 1];

            // Prompt for new value
            Console.Write($"Enter new value for cell '{selectedUser.NameU}': ");
            string newValue = Console.ReadLine();

            // Update the cell value
            selectedUser.Value.Val = newValue;
            Console.WriteLine("Cell value updated successfully.");

            // Ask if the user wants to edit another shape
            Console.Write("\nDo you want to edit another shape? (y/n): ");
            string continueResponse = Console.ReadLine();
            continueEditing = continueResponse?.Trim().ToLower() == "y";
        }

        // Determine output file path
        string directory = Path.GetDirectoryName(inputPath);
        string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
        string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_edited.vsdx");

        try
        {
            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"\nDiagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}