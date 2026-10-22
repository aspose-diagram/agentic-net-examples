using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Attempt to locate a rectangle shape.
            // For demonstration we use the first shape on the first page.
            // Adjust the logic as needed to target a specific rectangle.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape (replace with proper identification logic).
            Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);

            // TODO: Enable text wrapping for the shape.
            // The Aspose.Diagram API provides a TextBlock cell collection where the
            // Wrap property can be set (e.g., shape.TextBlock.Wrap = BOOL.True;).
            // Insert the appropriate code here once the correct property/method is identified.

            // Example of adding long text to demonstrate wrapping (optional).
            shape.Text.Value.Add(new Txt("This is a very long sentence that should automatically wrap inside the rectangle shape to improve readability."));

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}