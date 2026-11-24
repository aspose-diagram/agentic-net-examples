using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Define custom page size (in inches).
        double customWidth = 11.0;   // Example width.
        double customHeight = 8.5;   // Example height.

        try
        {
            // Load the source diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first (foreground) page.
                Page page = diagram.Pages[0];

                // Set custom page dimensions.
                page.PageSheet.PageProps.PageWidth.Value = customWidth;
                page.PageSheet.PageProps.PageHeight.Value = customHeight;

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            // Verify that the saved file exists.
            if (!File.Exists(outputPath))
            {
                Console.Error.WriteLine($"Failed to create output file: {outputPath}");
                return;
            }

            // Re-open the saved diagram to validate page size retention.
            using (Diagram savedDiagram = new Diagram(outputPath))
            {
                Page savedPage = savedDiagram.Pages[0];

                double savedWidth = savedPage.PageSheet.PageProps.PageWidth.Value;
                double savedHeight = savedPage.PageSheet.PageProps.PageHeight.Value;

                // Compare saved dimensions with the original custom dimensions.
                const double tolerance = 0.001; // Allow minimal floating‑point variance.
                bool widthMatches = Math.Abs(savedWidth - customWidth) < tolerance;
                bool heightMatches = Math.Abs(savedHeight - customHeight) < tolerance;

                if (widthMatches && heightMatches)
                {
                    Console.WriteLine("Success: Custom page size retained after saving and reloading.");
                }
                else
                {
                    string message = $"Failure: Page size mismatch. Expected ({customWidth} x {customHeight}), " +
                                     $"but got ({savedWidth} x {savedHeight}).";
                    Console.Error.WriteLine(message);
                    throw new Exception(message);
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}