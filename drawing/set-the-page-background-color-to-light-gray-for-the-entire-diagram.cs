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
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate center position for the background rectangle
                double centerX = pageWidth / 2.0;
                double centerY = pageHeight / 2.0;

                // Add a rectangle shape that spans the entire page
                // The fourth argument 'false' indicates that the shape is not a calculation shape
                long shapeId = page.AddShape(centerX, centerY, "Rectangle", false);
                Shape backgroundShape = page.Shapes.GetShape(shapeId);

                // Set the rectangle size to match the page size
                backgroundShape.XForm.Width.Value = pageWidth;
                backgroundShape.XForm.Height.Value = pageHeight;

                // Set fill to solid light gray (hex #D3D3D3)
                backgroundShape.Fill.FillPattern.Value = 1; // Solid fill
                backgroundShape.Fill.FillForegnd.Value = "#D3D3D3";

                // Optional: remove outline by setting line pattern to none
                backgroundShape.Line.LinePattern.Value = 0;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up
            diagram.Dispose();

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}