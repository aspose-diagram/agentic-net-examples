using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputJpegPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages to find background pages
            foreach (Page page in diagram.Pages)
            {
                // Check if the page is a background page
                if (page.Background == BOOL.True)
                {
                    // Apply a visual effect to each shape on the background page.
                    // Aspose.Diagram does not provide a direct blur filter, so we simulate
                    // a softened appearance by setting a light gray fill color.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a fill pattern; set to solid fill (value 1)
                        shape.Fill.FillPattern.Value = 1;

                        // Apply a light gray color to simulate a blurred look
                        shape.Fill.FillForegnd.Value = "#CCCCCC";
                    }
                }
            }

            // Configure high‑quality JPEG export options
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
            saveOptions.JpegQuality = 90;          // High quality (0‑100)
            saveOptions.Resolution = 300f;         // 300 DPI for better detail
            saveOptions.PageIndex = 0;             // Export the first page (foreground)
            saveOptions.PageCount = 1;             // Export a single page

            // Save the diagram as a JPEG image
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Diagram exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}