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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page (page index 0).
            Page page = diagram.Pages[0];

            // Add a new rectangle shape at coordinates (2, 3).
            // The fourth argument 'isCalculate' is set to false as per API requirements.
            long shapeId = page.AddShape(2.0, 3.0, "Rectangle", false);

            // Retrieve the newly added shape for further configuration.
            Shape shape = page.Shapes.GetShape(shapeId);

            // Determine a unique ID by finding the maximum existing shape ID on the page.
            long maxId = 0;
            foreach (Shape s in page.Shapes)
            {
                if (s.ID > maxId)
                    maxId = s.ID;
            }

            // Assign a unique ID (max existing ID + 1) to the new shape.
            shape.ID = maxId + 1;

            // Explicitly set the position (PinX, PinY) to (2, 3) – redundant but ensures correctness.
            shape.XForm.PinX.Value = 2.0;
            shape.XForm.PinY.Value = 3.0;

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Second rectangle shape added successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}