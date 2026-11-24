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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDirectory>");
            return;
        }

        string inputPath = args[0];
        string outputDir = args[1];

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: check output directory existence
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Log API version and build number before any save operation
                LogMetadata(diagram, "Initial load");

                // Example modification: change the title property (optional)
                diagram.DocumentProps.Title = "Processed Diagram";

                // Save as VSDX
                string vsdxPath = Path.Combine(outputDir, "output.vsdx");
                diagram.Save(vsdxPath, SaveFileFormat.Vsdx);
                LogMetadata(diagram, $"Saved VSDX to {vsdxPath}");

                // Save as PDF using PdfSaveOptions
                string pdfPath = Path.Combine(outputDir, "output.pdf");
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Optional: set default font for PDF export
                pdfOptions.DefaultFont = "Arial";
                diagram.Save(pdfPath, pdfOptions);
                LogMetadata(diagram, $"Saved PDF to {pdfPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any Aspose or IO errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Logs the diagram's API version and build number with a custom message.
    /// </summary>
    /// <param name="diagram">The loaded Diagram instance.</param>
    /// <param name="context">Description of the operation being logged.</param>
    private static void LogMetadata(Diagram diagram, string context)
    {
        // diagram.Version returns a string representing the Visio version used to create the file
        string version = diagram.Version;

        // BuildNumberCreated may be an integer or string; convert to string for safe logging
        string buildNumber = diagram.DocumentProps.BuildNumberCreated?.ToString() ?? "N/A";

        Console.WriteLine($"[{DateTime.Now}] {context}: Version = {version}, BuildNumberCreated = {buildNumber}");
    }
}