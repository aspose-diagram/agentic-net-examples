using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expecting: <inputVisioPath> <outputVisioPath> <targetPageWidthInInches> <targetPageHeightInInches>
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <targetPageWidthInInches> <targetPageHeightInInches>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string widthArg = args[2];
        string heightArg = args[3];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse target page dimensions
        if (!double.TryParse(widthArg, out double targetWidth) || targetWidth <= 0)
        {
            Console.Error.WriteLine("Invalid target page width.");
            return;
        }

        if (!double.TryParse(heightArg, out double targetHeight) || targetHeight <= 0)
        {
            Console.Error.WriteLine("Invalid target page height.");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve original page dimensions (in inches)
                double originalWidth = page.PageSheet.PageProps.PageWidth.Value;
                double originalHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Avoid division by zero
                if (originalWidth == 0 || originalHeight == 0)
                {
                    Console.Error.WriteLine("Encountered a page with zero width or height; skipping scaling for this page.");
                    continue;
                }

                // Compute scaling factors for X and Y dimensions
                double scaleX = targetWidth / originalWidth;
                double scaleY = targetHeight / originalHeight;

                // Scale each shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Scale shape size
                    shape.XForm.Width.Value *= scaleX;
                    shape.XForm.Height.Value *= scaleY;

                    // Scale shape position to keep relative layout
                    shape.XForm.PinX.Value *= scaleX;
                    shape.XForm.PinY.Value *= scaleY;
                }

                // Update the page size to the target dimensions
                page.PageSheet.PageProps.PageWidth.Value = targetWidth;
                page.PageSheet.PageProps.PageHeight.Value = targetHeight;
            }

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}