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
            // Load the existing Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Get the first page (or active page) where the new shape will be added.
                Page page = diagram.ActivePage;

                // Add a new rectangle shape to the page.
                // Parameters: pinX, pinY, master name, isCalculate (false = no immediate layout).
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);

                // Retrieve the newly added shape using its ID.
                Shape shape = page.Shapes.GetShape(shapeId);

                // Modify the shape's fill color.
                shape.Fill.FillForegnd.Value = "#FF0000"; // Red fill.

                // Update the shape's text.
                shape.Text.Value.Clear();                     // Remove any existing text.
                shape.Text.Value.Add(new Txt("Hello Aspose")); // Add new text.

                // Refresh the shape's data to ensure changes are applied.
                shape.RefreshData();

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}