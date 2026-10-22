using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (assuming the rectangle is on the first page).
            Page page = diagram.Pages[0];

            // Iterate through shapes to find a rectangle shape.
            // For simplicity, we select the first shape; adjust logic as needed.
            Shape rectangleShape = null;
            foreach (Shape shape in page.Shapes)
            {
                // Check if the shape's master name is "Rectangle" (common master name).
                // Shape.NameU may contain the master name; use it if available.
                if (!string.IsNullOrEmpty(shape.NameU) && shape.NameU.Equals("Rectangle", StringComparison.OrdinalIgnoreCase))
                {
                    rectangleShape = shape;
                    break;
                }
            }

            // Fallback: if no rectangle found by name, use the first shape.
            if (rectangleShape == null && page.Shapes.Count > 0)
            {
                rectangleShape = page.Shapes[0];
            }

            if (rectangleShape == null)
            {
                Console.Error.WriteLine("No shape found in the diagram to modify.");
                return;
            }

            // Set the text direction of the shape's text block to vertical.
            // This configures the text for a column layout.
            rectangleShape.TextBlock.TextDirection.Value = TextDirectionValue.Vertical;

            // Optionally, add sample text to demonstrate the vertical direction.
            rectangleShape.Text.Value.Clear();
            rectangleShape.Text.Value.Add(new Txt("Column Text"));

            // Save the modified diagram to the output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}