using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Prompt for the Visio file path.
        Console.Write("Enter the path to the Visio file: ");
        string filePath = Console.ReadLine();

        // Guard: ensure the file exists.
        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
        {
            Console.Error.WriteLine("Error: The specified file does not exist.");
            return;
        }

        // Prompt for the user-defined cell name pattern to filter.
        Console.Write("Enter the user-defined cell name pattern (case-insensitive substring): ");
        string pattern = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(pattern))
        {
            Console.Error.WriteLine("Error: The pattern cannot be empty.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(filePath);

            Console.WriteLine($"Processing diagram: {Path.GetFileName(filePath)}");
            Console.WriteLine($"Looking for user-defined cells containing pattern: \"{pattern}\"");
            Console.WriteLine();

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check each user-defined cell (User) for a matching name.
                    bool hasMatchingUser = false;
                    foreach (User user in shape.Users)
                    {
                        // Compare both Name and NameU for robustness.
                        if ((user.Name != null && user.Name.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (user.NameU != null && user.NameU.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) >= 0))
                        {
                            hasMatchingUser = true;
                            // Output details of the matching user-defined cell.
                            Console.WriteLine($"Page {page.ID} - Shape ID {shape.ID}");
                            Console.WriteLine($"  Shape Name : {shape.Name}");
                            Console.WriteLine($"  Master Name: {shape.Master?.Name ?? "N/A"}");
                            Console.WriteLine($"  Matching User Cell:");
                            Console.WriteLine($"    Name  : {user.Name}");
                            Console.WriteLine($"    NameU : {user.NameU}");
                            Console.WriteLine($"    Value : {user.Value?.Val}");
                            Console.WriteLine($"    Prompt: {user.Prompt?.Value}");
                            Console.WriteLine();
                        }
                    }

                    // If needed, additional processing for shapes that matched can be placed here.
                    // For this example, we simply report the matching shapes.
                }
            }

            Console.WriteLine("Processing completed.");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}