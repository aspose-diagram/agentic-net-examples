using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Verify that the diagram contains at least one page (page zero)
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Add a rectangle shape on page zero at coordinates (2, 3)
            // AddShape returns the newly created shape's unique ID (long)
            long shapeId = diagram.AddShape(2.0, 3.0, "Rectangle", 0);

            // Retrieve the shape instance for any further modifications (optional)
            Shape rectShape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Output the assigned unique ID
            Console.WriteLine($"Added rectangle shape with ID: {shapeId}");

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}