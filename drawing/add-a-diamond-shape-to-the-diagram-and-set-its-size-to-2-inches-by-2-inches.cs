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

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define the master name for a diamond shape (standard Visio master).
                const string diamondMasterName = "Diamond";

                // Add the diamond shape to the first page (page index 0).
                // PinX and PinY are the center coordinates; using (2,2) inches as an example.
                long shapeId = diagram.AddShape(2.0, 2.0, diamondMasterName, 0);

                // Retrieve the newly added shape from the page's shape collection.
                Shape diamondShape = diagram.Pages[0].Shapes.GetShape(shapeId);

                // Set the shape size to 2 inches width and 2 inches height.
                diamondShape.XForm.Width.Value = 2.0;
                diamondShape.XForm.Height.Value = 2.0;

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diamond shape added and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}