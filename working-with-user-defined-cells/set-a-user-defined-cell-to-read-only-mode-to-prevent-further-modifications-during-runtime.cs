using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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

            // Flag to indicate whether we have processed a shape.
            bool shapeProcessed = false;

            // Iterate through pages and shapes to find the first shape.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Create a new user‑defined cell.
                    User userCell = new User();
                    userCell.Name = "MyReadOnlyCell";          // Row name.
                    userCell.Value.Val = "12345";              // Cell value.
                    userCell.Prompt.Value = "Read‑only value"; // Optional description.

                    // Add the user‑defined cell to the shape.
                    shape.Users.Add(userCell);

                    // Lock custom properties (user‑defined cells) for this shape.
                    // This makes the cell read‑only during runtime.
                    shape.Protection.LockCustProp.Value = BOOL.True;

                    shapeProcessed = true;
                    break; // Process only the first shape for demonstration.
                }

                if (shapeProcessed)
                    break;
            }

            if (!shapeProcessed)
            {
                Console.Error.WriteLine("No shapes found in the diagram to modify.");
                return;
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}