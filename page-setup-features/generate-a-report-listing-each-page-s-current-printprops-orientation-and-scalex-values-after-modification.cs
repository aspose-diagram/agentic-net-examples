using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // -----------------------------------------------------------------
                // Modify each page's PrintProps: set orientation to Landscape and
                // ScaleX to 0.75 (75 %).
                // -----------------------------------------------------------------
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    page.PageSheet.PrintProps.ScaleX.Value = 0.75;
                }

                // -----------------------------------------------------------------
                // Generate a report of the current PrintProps after modification.
                // -----------------------------------------------------------------
                Console.WriteLine("Page PrintProps Report:");
                int pageIndex = 0;
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    PrintPageOrientationValue orientation = page.PageSheet.PrintProps.PrintPageOrientation.Value;
                    double scaleX = page.PageSheet.PrintProps.ScaleX.Value;
                    Console.WriteLine($"Page {pageIndex}: Orientation = {orientation}, ScaleX = {scaleX}");
                    pageIndex++;
                }

                // -----------------------------------------------------------------
                // Optionally save the modified diagram to a new file.
                // -----------------------------------------------------------------
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(inputPath) ?? string.Empty,
                    Path.GetFileNameWithoutExtension(inputPath) + "_modified.vsdx");

                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Modified diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}