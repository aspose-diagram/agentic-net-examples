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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        // If output path is not provided, create one by appending "_modified" before the extension.
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_modified" + Path.GetExtension(inputPath));

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

            // Iterate over all pages and apply the required print settings.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Explicitly type the iterator as Page.
                Page page = diagram.Pages[i];

                // Set page orientation to Portrait.
                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;

                // Set horizontal scaling factor to 1.2 (120%).
                page.PageSheet.PrintProps.ScaleX.Value = 1.2;
            }

            // Save the modified diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up resources.
            diagram.Dispose();

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}