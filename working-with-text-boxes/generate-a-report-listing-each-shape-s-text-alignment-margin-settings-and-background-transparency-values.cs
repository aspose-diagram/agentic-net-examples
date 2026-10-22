using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve shape identifier and name for reporting.
                    string shapeId = shape.ID.ToString();
                    string shapeName = shape.NameU ?? string.Empty;

                    // Determine if the shape contains any text.
                    string shapeText = shape.Text.Value.Text ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(shapeText))
                    {
                        // Skip shapes without text content.
                        continue;
                    }

                    // ----- Extract required text block properties -----

                    // Text alignment (vertical alignment).
                    string verticalAlignment = shape.TextBlock.VerticalAlign.Value.ToString();

                    // Margin settings (values are in points).
                    double leftMargin = shape.TextBlock.LeftMargin.Value;
                    double rightMargin = shape.TextBlock.RightMargin.Value;
                    double topMargin = shape.TextBlock.TopMargin.Value;
                    double bottomMargin = shape.TextBlock.BottomMargin.Value;

                    // Background transparency (percentage 0‑100).
                    double backgroundTransparency = shape.TextBlock.TextBkgndTrans.Value;

                    // ----- Output the report for the current shape -----
                    Console.WriteLine($"Shape ID: {shapeId}, Name: {shapeName}");
                    Console.WriteLine($"  Text Alignment (Vertical): {verticalAlignment}");
                    Console.WriteLine($"  Margins - Left: {leftMargin}, Right: {rightMargin}, Top: {topMargin}, Bottom: {bottomMargin}");
                    Console.WriteLine($"  Background Transparency: {backgroundTransparency}%");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}