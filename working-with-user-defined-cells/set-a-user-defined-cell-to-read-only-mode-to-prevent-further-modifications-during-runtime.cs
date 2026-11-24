using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram does not contain any pages.");
                return;
            }

            // Get the first page.
            Page page = diagram.Pages[0];

            // Ensure the page contains at least one shape.
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page does not contain any shapes.");
                return;
            }

            // Get the first shape on the page.
            Shape shape = page.Shapes[0];

            // Create a new user‑defined cell.
            User userCell = new User();
            userCell.Name = "ReadOnlyCell";          // Row name.
            userCell.Value.Val = "800";              // Cell value as string.
            userCell.Prompt.Value = "Read‑only value"; // Optional description.

            // Add the user‑defined cell to the shape.
            shape.Users.Add(userCell);

            // Set the shape's protection to lock custom properties (user‑defined cells).
            // This makes the user‑defined cell read‑only at runtime.
            shape.Protection.LockCustProp.Value = BOOL.True;

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}