using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments: expect input diagram path and output diagram path.
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
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page (index 0).
            Page page = diagram.Pages[0];

            // Define the shape ID to modify. Adjust as needed.
            long shapeId = 1;

            // Retrieve the shape by its ID.
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found on page '{page.Name}'.");
                return;
            }

            // Skip deleted shapes.
            if (shape.Del == BOOL.True)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} is marked as deleted.");
                return;
            }

            // Set text orientation angle to 0 degrees (horizontal).
            shape.TextXForm.TxtAngle.Value = 0.0;

            // Align text to the left side of the shape.
            // According to Aspose.Diagram guidelines:
            //   TxtLocPinX = TxtWidth (local pin at the right edge of the text block)
            //   TxtPinX   = 0       (position the text block's left edge at the shape's left edge)
            shape.TextXForm.TxtLocPinX.Value = shape.TextXForm.TxtWidth.Value;
            shape.TextXForm.TxtPinX.Value = 0.0;

            // Save the modified diagram to the output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}