using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputXpsFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // ------------------------------------------------------------
            // Apply automatic spacing (auto‑spacing) to each page.
            // ------------------------------------------------------------
            AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions();
            // Optional: adjust spacing distances (in inches) if needed.
            // autoSpaceOptions.DistanceInHorizontal = 0.5;
            // autoSpaceOptions.DistanceInVertical = 0.5;

            // Iterate all pages and auto‑space their shapes.
            foreach (Page page in diagram.Pages)
            {
                page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);
            }

            // ------------------------------------------------------------
            // Configure XPS save options.
            // ------------------------------------------------------------
            XPSSaveOptions xpsOptions = new XPSSaveOptions();
            // Do not export hidden pages. (ExportHiddenPage expects a bool, not BOOL)
            xpsOptions.ExportHiddenPage = false;

            // ------------------------------------------------------------
            // Save the diagram as XPS.
            // ------------------------------------------------------------
            diagram.Save(outputPath, xpsOptions);

            Console.WriteLine($"Diagram successfully exported to XPS: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}
