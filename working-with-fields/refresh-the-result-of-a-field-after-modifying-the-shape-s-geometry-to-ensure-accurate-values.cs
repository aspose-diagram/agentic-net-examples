using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Access the first page
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the page
            // Shape IDs are stored as long; cast to int for GetShape
            long shapeId = page.Shapes[0].ID;
            Shape shape = page.Shapes.GetShape((int)shapeId);

            // --- Modify the shape's geometry ---
            // Example: change the width and height of the shape
            // This will affect the shape's geometry and may impact any attached fields
            shape.XForm.Width.Value = 2.0;   // Set width to 2 inches
            shape.XForm.Height.Value = 2.0;  // Set height to 2 inches

            // Refresh the shape's data so that any fields recalculate based on the new geometry
            shape.RefreshData();

            // Optional: verify that the shape now has the expected dimensions
            Console.WriteLine($"Shape ID {shape.ID} new size: Width={shape.XForm.Width.Value}, Height={shape.XForm.Height.Value}");

            // Save the modified diagram to a new file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}