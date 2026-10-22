using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page (index 0)
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            // Get page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Calculate center coordinates for the background rectangle
            double centerX = pageWidth / 2.0;
            double centerY = pageHeight / 2.0;

            // Add a rectangle shape that spans the entire page
            // Master name "Rectangle" is a built‑in Visio shape
            long rectShapeId = page.AddShape(centerX, centerY, "Rectangle", false);

            // Retrieve the newly added shape
            Shape backgroundShape = page.Shapes.GetShape(rectShapeId);

            // Set solid fill pattern
            backgroundShape.Fill.FillPattern.Value = 1; // Solid fill

            // Set the desired background color (hex string, e.g., light blue)
            backgroundShape.Fill.FillForegnd.Value = "#ADD8E6";

            // Remove the outline by setting line pattern to none
            backgroundShape.Line.LinePattern.Value = 0;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}