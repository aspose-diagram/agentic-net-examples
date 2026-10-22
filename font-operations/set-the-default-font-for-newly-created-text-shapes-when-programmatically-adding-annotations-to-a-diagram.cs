using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths (use defaults if not provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input diagram file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Set the global default font name for all newly created text shapes
        // This must be done before creating any new text shapes
        FontConfigs.DefaultFontName = "Arial";

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page where the annotation will be added
            Page page = diagram.Pages[0];

            // Add a new text shape (callout) to the page
            // Parameters: pinX, pinY, width, height, text
            double pinX = 2.0;   // horizontal position (in inches)
            double pinY = 2.0;   // vertical position (in inches)
            double width = 3.0;  // shape width (in inches)
            double height = 1.0; // shape height (in inches)
            string shapeText = "Sample annotation text";

            Shape textShape = page.AddText(pinX, pinY, width, height, shapeText);

            // Add a comment (annotation) linked to the newly created text shape
            string commentText = "This is a programmatically added annotation.";
            page.AddComment(textShape, commentText);

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}