using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    // Wrapper that logs old and new width before applying the change
    static void SetWidthWithLogging(Shape shape, double newWidth)
    {
        // Retrieve current width (in inches) from the shape's XForm
        double oldWidth = shape.XForm.Width.Value;
        Console.WriteLine($"Setting Width - Old: {oldWidth}, New: {newWidth}");
        // Apply the new width using the native SetWidth method
        shape.SetWidth(newWidth);
    }

    // Wrapper that logs old and new height before applying the change
    static void SetHeightWithLogging(Shape shape, double newHeight)
    {
        // Retrieve current height (in inches) from the shape's XForm
        double oldHeight = shape.XForm.Height.Value;
        Console.WriteLine($"Setting Height - Old: {oldHeight}, New: {newHeight}");
        // Apply the new height using the native SetHeight method
        shape.SetHeight(newHeight);
    }

    static void Main(string[] args)
    {
        // Expect input diagram path and output diagram path as arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
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
                // For demonstration, operate on the first page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Define the shape ID to modify; adjust as needed
                long shapeId = 1; // Example shape ID
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on the first page.");
                    return;
                }

                // Example new dimensions (in inches)
                double newWidth = 2.0;
                double newHeight = 1.5;

                // Apply width and height changes using the logging wrappers
                SetWidthWithLogging(shape, newWidth);
                SetHeightWithLogging(shape, newHeight);

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during Aspose operations
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}