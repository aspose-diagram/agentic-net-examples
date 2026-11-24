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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath> [columnWidthInInches]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        double columnWidth = 2.0; // default column width in inches

        if (args.Length >= 3 && double.TryParse(args[2], out double parsedWidth) && parsedWidth > 0)
        {
            columnWidth = parsedWidth;
        }

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
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

            // Retrieve page width (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;

            // Compute the number of columns that fit into the page width
            int columnCount = Math.Max(1, (int)(pageWidth / columnWidth));

            // Define a simple margin and row height (using the same value as column width for a square grid)
            double margin = 0.5; // inches
            double rowHeight = columnWidth;

            // Position each shape in a grid layout
            int shapeCount = page.Shapes.Count;
            for (int i = 0; i < shapeCount; i++)
            {
                Shape shape = page.Shapes[i];

                // Determine row and column for the current shape
                int row = i / columnCount;
                int col = i % columnCount;

                // Calculate the center position for the shape
                double pinX = margin + col * columnWidth + columnWidth / 2.0;
                double pinY = margin + row * rowHeight + rowHeight / 2.0;

                // Apply the new position
                shape.XForm.PinX.Value = pinX;
                shape.XForm.PinY.Value = pinY;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}