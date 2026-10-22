using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, shape name to export, output HTML file path.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> <shapeName> <outputHtmlPath>");
            return;
        }

        string inputPath = args[0];
        string shapeName = args[1];
        string outputPath = args[2];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Use the active page (or you could select a specific page by index).
            Page page = diagram.ActivePage;

            // Locate the shape with the specified NameU.
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                // NameU holds the universal name of the shape.
                if (shape.NameU != null && shape.NameU.Equals(shapeName, StringComparison.OrdinalIgnoreCase))
                {
                    targetShape = shape;
                    break;
                }
            }

            // If the shape was not found, report and exit.
            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape \"{shapeName}\" not found in the diagram.");
                return;
            }

            // Prepare HTML save options (default options preserve geometry and style).
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Export the selected shape to HTML.
            targetShape.ToHTML(outputPath, htmlOptions);

            Console.WriteLine($"Shape \"{shapeName}\" successfully exported to HTML: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console.
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}