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
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputXpsFilePath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Determine output path. If not supplied, replace extension with .xps.
        string outputPath = args.Length >= 2 ? args[1] : Path.ChangeExtension(inputPath, ".xps");

        try
        {
            // Load the Visio diagram inside a using block (ensures proper disposal).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Perform auto‑spacing on each page before exporting.
                // ------------------------------------------------------------
                AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions();
                // Example spacing distances (in inches). Adjust as needed.
                autoSpaceOptions.DistanceInHorizontal = 0.5;
                autoSpaceOptions.DistanceInVertical = 0.5;

                foreach (Page page in diagram.Pages)
                {
                    // Auto‑space shapes on the current page using the defined options.
                    page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);
                }

                // ------------------------------------------------------------
                // Configure XPS save options.
                // ------------------------------------------------------------
                XPSSaveOptions xpsOptions = new XPSSaveOptions();
                // Do not export hidden pages (plain bool, not BOOL enum).
                xpsOptions.ExportHiddenPage = false;
                // Explicitly set the target format (optional but clarifies intent).
                xpsOptions.SaveFormat = SaveFileFormat.Xps;

                // ------------------------------------------------------------
                // Save the diagram as XPS.
                // ------------------------------------------------------------
                diagram.Save(outputPath, xpsOptions);
                Console.WriteLine($"Diagram successfully exported to XPS: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors and report them.
            Console.Error.WriteLine($"An error occurred during export: {ex.Message}");
        }
    }
}