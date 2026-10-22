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

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Access the first page.
                Page page = diagram.Pages[0];

                // Retrieve a shape by its ID. Here we use ID = 1 as an example.
                // Adjust the ID as needed for your specific diagram.
                long shapeId = 1;
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on the first page.");
                    return;
                }

                // Log current dimensions.
                double currentWidth = shape.XForm.Width.Value;
                double currentHeight = shape.XForm.Height.Value;
                Console.WriteLine($"Current dimensions - Width: {currentWidth}, Height: {currentHeight}");

                // Define new dimensions (example: increase each by 1.0 units).
                double newWidth = currentWidth + 1.0;
                double newHeight = currentHeight + 1.0;

                // Apply width and height changes using the logging wrappers.
                SetShapeWidth(shape, newWidth);
                SetShapeHeight(shape, newHeight);

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Sets the width of a shape while logging the old and new values.
    /// </summary>
    /// <param name="shape">The shape to modify.</param>
    /// <param name="newWidth">The desired new width.</param>
    private static void SetShapeWidth(Shape shape, double newWidth)
    {
        // Retrieve the existing width.
        double oldWidth = shape.XForm.Width.Value;
        // Log the change.
        Console.WriteLine($"Changing width from {oldWidth} to {newWidth}");
        // Apply the new width.
        shape.SetWidth(newWidth);
    }

    /// <summary>
    /// Sets the height of a shape while logging the old and new values.
    /// </summary>
    /// <param name="shape">The shape to modify.</param>
    /// <param name="newHeight">The desired new height.</param>
    private static void SetShapeHeight(Shape shape, double newHeight)
    {
        // Retrieve the existing height.
        double oldHeight = shape.XForm.Height.Value;
        // Log the change.
        Console.WriteLine($"Changing height from {oldHeight} to {newHeight}");
        // Apply the new height.
        shape.SetHeight(newHeight);
    }
}