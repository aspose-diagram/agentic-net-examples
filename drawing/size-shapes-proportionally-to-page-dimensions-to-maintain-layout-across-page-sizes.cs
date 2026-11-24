using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expecting four arguments: input diagram path, output diagram path, new page width (in inches), new page height (in inches)
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <program> <inputPath> <outputPath> <newPageWidthInches> <newPageHeightInches>");
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

        // Parse new page dimensions
        if (!double.TryParse(widthArg, out double newPageWidth) || newPageWidth <= 0)
        {
            Console.Error.WriteLine("Invalid new page width. It must be a positive number.");
            return;
        }

        if (!double.TryParse(heightArg, out double newPageHeight) || newPageHeight <= 0)
        {
            Console.Error.WriteLine("Invalid new page height. It must be a positive number.");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve original page dimensions (in inches)
                double originalWidth = page.PageSheet.PageProps.PageWidth.Value;
                double originalHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Avoid division by zero
                if (originalWidth == 0 || originalHeight == 0)
                {
                    Console.Error.WriteLine("Encountered a page with zero width or height. Skipping scaling for this page.");
                    continue;
                }

                // Calculate scaling factors for width and height
                double scaleX = newPageWidth / originalWidth;
                double scaleY = newPageHeight / originalHeight;

                // Scale each shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Scale shape size
                    shape.XForm.Width.Value *= scaleX;
                    shape.XForm.Height.Value *= scaleY;

                    // Scale shape position
                    shape.XForm.PinX.Value *= scaleX;
                    shape.XForm.PinY.Value *= scaleY;
                }

                // Update the page size to the new dimensions
                page.PageSheet.PageProps.PageWidth.Value = newPageWidth;
                page.PageSheet.PageProps.PageHeight.Value = newPageHeight;
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}