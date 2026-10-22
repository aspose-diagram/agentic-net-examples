using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Prompt for input diagram path if not provided as argument
        string inputPath = args.Length > 0 ? args[0] : "";
        if (string.IsNullOrWhiteSpace(inputPath))
        {
            Console.Write("Enter the path to the Visio diagram file: ");
            inputPath = Console.ReadLine()?.Trim() ?? "";
        }

        // Guard: ensure the file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
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

        // Build a flat list of all shapes with their page reference for easy selection
        var shapeEntries = new List<(Page page, Shape shape)>();
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                shapeEntries.Add((page, shape));
            }
        }

        if (shapeEntries.Count == 0)
        {
            Console.WriteLine("The diagram contains no shapes.");
            return;
        }

        while (true)
        {
            Console.WriteLine("\n--- Shape Selection ---");
            // List shapes with an index
            for (int i = 0; i < shapeEntries.Count; i++)
            {
                var entry = shapeEntries[i];
                Console.WriteLine($"{i + 1}. Page: {entry.page.Name}, Shape ID: {entry.shape.ID}, Name: {entry.shape.Name}");
            }
            Console.WriteLine("0. Exit");

            Console.Write("Select a shape by number: ");
            if (!int.TryParse(Console.ReadLine(), out int shapeChoice) || shapeChoice < 0 || shapeChoice > shapeEntries.Count)
            {
                Console.WriteLine("Invalid selection. Try again.");
                continue;
            }

            if (shapeChoice == 0)
                break; // Exit the program

            var selectedEntry = shapeEntries[shapeChoice - 1];
            Shape selectedShape = selectedEntry.shape;

            // List user-defined cells for the selected shape
            Console.WriteLine($"\nUser-defined cells for Shape ID {selectedShape.ID}:");
            if (selectedShape.Users.Count == 0)
            {
                Console.WriteLine("  (No user-defined cells)");
            }
            else
            {
                for (int u = 0; u < selectedShape.Users.Count; u++)
                {
                    User user = selectedShape.Users[u];
                    Console.WriteLine($"{u + 1}. Name: {user.Name}, NameU: {user.NameU}, Value: {user.Value.Val}, Prompt: {user.Prompt.Value}");
                }
            }

            Console.WriteLine("0. Return to shape list");
            Console.Write("Enter the number of the user-defined cell to edit (or 0 to go back): ");
            if (!int.TryParse(Console.ReadLine(), out int userChoice) || userChoice < 0 || userChoice > selectedShape.Users.Count)
            {
                Console.WriteLine("Invalid selection. Returning to shape list.");
                continue;
            }

            if (userChoice == 0)
                continue; // Back to shape selection

            // Edit the selected user-defined cell
            User targetUser = selectedShape.Users[userChoice - 1];
            Console.WriteLine($"Current value of '{targetUser.Name}' is: {targetUser.Value.Val}");
            Console.Write("Enter new value: ");
            string newValue = Console.ReadLine() ?? "";
            targetUser.Value.Val = newValue;
            Console.WriteLine("Value updated.");

            // Ask if the user wants to continue editing or save and exit
            Console.Write("\nDo you want to edit another cell? (y/n): ");
            string continueChoice = Console.ReadLine()?.Trim().ToLower() ?? "n";
            if (continueChoice != "y")
                break;
        }

        // Prompt for output path
        Console.Write("\nEnter the path to save the modified diagram (e.g., output.vsdx): ");
        string outputPath = Console.ReadLine()?.Trim() ?? "";
        if (string.IsNullOrWhiteSpace(outputPath))
        {
            Console.Error.WriteLine("No output path provided. Exiting without saving.");
            return;
        }

        // Ensure the directory exists
        string outputDir = Path.GetDirectoryName(outputPath) ?? "";
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Error: Output directory does not exist - {outputDir}");
            return;
        }

        try
        {
            // Save the diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}