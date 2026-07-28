using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the Visio file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine output image file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output.png";

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate over all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify connector shapes: 1‑D shapes have OneD == true
                    if (shape.OneD)
                    {
                        // Apply a simple shadow effect to the connector
                        shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;   // Enable simple shadow
                        shape.Fill.ShdwForegnd.Value = "#000000";                     // Shadow color: black
                        shape.Fill.ShdwForegndTrans.Value = 0.3;                     // 30 % transparency
                        shape.Fill.ShapeShdwOffsetX.Value = 0.1;                     // Horizontal offset (in inches)
                        shape.Fill.ShapeShdwOffsetY.Value = 0.1;                     // Vertical offset (in inches)
                    }
                }
            }

            // Configure high‑resolution image export options (PNG format)
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
            {
                // Set resolution to 300 dpi for high quality
                Resolution = 300f,
                // Export the first page (index 0); change if multiple pages are needed
                PageIndex = 0,
                // Ensure hidden pages are not exported
                ExportHiddenPage = false
            };

            // Save the diagram as a high‑resolution PNG image
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Shadow applied to connectors and image saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}