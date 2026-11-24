using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (can be overridden via command‑line arguments)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the source Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Assume the shape is on the first page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The diagram contains no pages.");
                return;
            }
            Page page = diagram.Pages[0];

            // Retrieve the shape with ID 10
            long targetShapeId = 10L;
            Shape shape = page.Shapes.GetShape(targetShapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Error: No shape with ID {targetShapeId} was found on the page.");
                return;
            }

            // Skip the shape if it is marked as deleted
            if (shape.Del == BOOL.True)
            {
                Console.Error.WriteLine($"Error: Shape ID {targetShapeId} is marked as deleted.");
                return;
            }

            // Locate the built‑in 'Heading 1' stylesheet
            StyleSheet headingStyle = null;
            foreach (StyleSheet ss in diagram.StyleSheets)
            {
                if (ss.Name != null && ss.Name.Equals("Heading 1", StringComparison.OrdinalIgnoreCase))
                {
                    headingStyle = ss;
                    break;
                }
            }

            if (headingStyle == null)
            {
                Console.Error.WriteLine("Error: 'Heading 1' stylesheet not found in the document.");
                return;
            }

            // Apply the stylesheet to the shape's text, fill, and line styles
            shape.TextStyle = headingStyle;
            shape.FillStyle = headingStyle;
            shape.LineStyle = headingStyle;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Successfully applied 'Heading 1' style to shape ID {targetShapeId} and saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Report any exceptions that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}