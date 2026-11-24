using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Assume the rectangle shape has ID 1 (adjust as needed)
                long rectangleShapeId = 1;

                // Retrieve the shape from the first page
                Page page = diagram.Pages[0];
                Shape rectangle = page.Shapes.GetShape(rectangleShapeId);

                // Verify the shape exists
                if (rectangle == null)
                {
                    Console.Error.WriteLine($"Shape with ID {rectangleShapeId} not found.");
                    return;
                }

                // Double the width and height while keeping aspect ratio
                rectangle.XForm.Width.Value *= 2.0;
                rectangle.XForm.Height.Value *= 2.0;

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}