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

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Define desired column width (including spacing) in inches
            const double columnWidth = 2.0; // Adjust as needed
            const double rowHeight = 1.5;   // Adjust as needed

            // Compute number of columns that fit into the page width
            int columnCount = (int)Math.Floor(pageWidth / columnWidth);
            if (columnCount < 1) columnCount = 1; // Ensure at least one column

            // Position shapes in a simple left‑to‑right, top‑to‑bottom grid
            int shapeIndex = 0;
            for (int i = 0; i < page.Shapes.Count; i++)
            {
                // Retrieve the shape
                Shape shape = page.Shapes[i];

                // Calculate row and column for this shape
                int row = shapeIndex / columnCount;
                int col = shapeIndex % columnCount;

                // Compute PinX and PinY (center of the shape)
                double pinX = (col + 0.5) * columnWidth;
                double pinY = pageHeight - ((row + 0.5) * rowHeight); // Y origin is at bottom

                // Apply new position
                shape.XForm.PinX.Value = pinX;
                shape.XForm.PinY.Value = pinY;

                shapeIndex++;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}