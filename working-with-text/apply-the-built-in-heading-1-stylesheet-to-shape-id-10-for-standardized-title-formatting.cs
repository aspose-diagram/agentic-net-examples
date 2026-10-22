using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as the first argument.");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define output path (same directory, new file name)
        string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "output.vsdx");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the built‑in "Heading 1" stylesheet
            StyleSheet headingStyle = null;
            foreach (StyleSheet ss in diagram.StyleSheets)
            {
                if (ss.Name == "Heading 1")
                {
                    headingStyle = ss;
                    break;
                }
            }

            if (headingStyle == null)
            {
                Console.Error.WriteLine("The 'Heading 1' stylesheet was not found in the document.");
                return;
            }

            // Assume the shape is on the first page; retrieve the page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            // Retrieve shape with ID 10
            long targetShapeId = 10;
            Shape shape = page.Shapes.GetShape(targetShapeId);

            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {targetShapeId} was not found on the first page.");
                return;
            }

            // Ensure the shape is not deleted
            if (shape.Del == BOOL.True)
            {
                Console.Error.WriteLine($"Shape with ID {targetShapeId} is marked as deleted.");
                return;
            }

            // Apply the Heading 1 stylesheet to text, fill, and line styles
            shape.TextStyle = headingStyle;
            shape.FillStyle = headingStyle;
            shape.LineStyle = headingStyle;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}