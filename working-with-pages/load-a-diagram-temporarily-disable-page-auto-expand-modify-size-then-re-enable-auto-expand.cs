using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths (command‑line arguments or defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

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

            // Ensure the diagram contains at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page
            Page page = diagram.Pages[0];

            // ------------------------------------------------------------
            // Temporarily disable page auto‑expand
            // ------------------------------------------------------------
            // Set DrawingResizeType to NotAutomatically to prevent auto‑expansion
            page.PageSheet.PageProps.DrawingResizeType.Value = DrawingResizeTypeValue.NotAutomatically;

            // ------------------------------------------------------------
            // Modify the page size (width and height in inches)
            // ------------------------------------------------------------
            double newWidth = 20.0;   // Desired page width in inches
            double newHeight = 15.0;  // Desired page height in inches

            page.PageSheet.PageProps.PageWidth.Value = newWidth;
            page.PageSheet.PageProps.PageHeight.Value = newHeight;

            // ------------------------------------------------------------
            // Re‑enable page auto‑expand
            // ------------------------------------------------------------
            // Restore DrawingResizeType to Automatically
            page.PageSheet.PageProps.DrawingResizeType.Value = DrawingResizeTypeValue.Automatically;

            // ------------------------------------------------------------
            // Save the modified diagram
            // ------------------------------------------------------------
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