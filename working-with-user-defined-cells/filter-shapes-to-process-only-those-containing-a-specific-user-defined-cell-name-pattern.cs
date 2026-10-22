using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // ------------------------------------------------------------
        // Get diagram file path from arguments or prompt the user.
        // ------------------------------------------------------------
        string diagramPath;
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            diagramPath = args[0];
        }
        else
        {
            Console.Write("Enter the full path to the Visio diagram file: ");
            diagramPath = Console.ReadLine();
        }

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // ------------------------------------------------------------
        // Get the user-defined cell name pattern to filter shapes.
        // ------------------------------------------------------------
        string pattern;
        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
        {
            pattern = args[1];
        }
        else
        {
            Console.Write("Enter the user-defined cell name pattern to filter (case-insensitive): ");
            pattern = Console.ReadLine();
        }

        if (string.IsNullOrWhiteSpace(pattern))
        {
            Console.Error.WriteLine("Error: No pattern provided.");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // Load the Visio diagram.
            // ------------------------------------------------------------
            Diagram diagram = new Diagram(diagramPath);

            // ------------------------------------------------------------
            // Iterate through all pages and shapes, processing only those
            // that contain a user-defined cell whose Name or NameU matches
            // the supplied pattern.
            // ------------------------------------------------------------
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    bool matchesPattern = false;

                    // Iterate over all user-defined cells of the shape.
                    foreach (User userCell in shape.Users)
                    {
                        // Check both Name and NameU for the pattern (case-insensitive).
                        if (!string.IsNullOrEmpty(userCell.Name) &&
                            userCell.Name.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            matchesPattern = true;
                            break;
                        }

                        if (!string.IsNullOrEmpty(userCell.NameU) &&
                            userCell.NameU.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            matchesPattern = true;
                            break;
                        }
                    }

                    // If the shape contains a matching user-defined cell, process it.
                    if (matchesPattern)
                    {
                        // Example processing: output shape details to console.
                        Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}, Shape Name: {shape.NameU}");

                        // Additional custom processing can be placed here.
                        // For demonstration, we will list all matching user-defined cells.
                        foreach (User userCell in shape.Users)
                        {
                            bool nameMatches = (!string.IsNullOrEmpty(userCell.Name) &&
                                                userCell.Name.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) >= 0) ||
                                               (!string.IsNullOrEmpty(userCell.NameU) &&
                                                userCell.NameU.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) >= 0);

                            if (nameMatches)
                            {
                                Console.WriteLine($"  User Cell - Name: {userCell.Name}, NameU: {userCell.NameU}, Value: {userCell.Value.Val}, Prompt: {userCell.Prompt.Value}");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // ------------------------------------------------------------
            // Handle any exceptions thrown by Aspose.Diagram operations.
            // ------------------------------------------------------------
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}