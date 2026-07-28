using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output directories (defaults if not provided)
        string inputFolder = args.Length > 0 ? args[0] : "InputDiagrams";
        string outputFolder = args.Length > 1 ? args[1] : "OutputDiagrams";

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Supported Visio file extensions
        string[] supportedExtensions = new[] { ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx", ".vssx", ".vstx", ".vsdm", ".vssm", ".vstm", ".vss", ".vst" };

        // Process each file in the input folder
        foreach (string filePath in Directory.GetFiles(inputFolder))
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (Array.IndexOf(supportedExtensions, extension) < 0)
            {
                // Skip unsupported files
                continue;
            }

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Update the footer with the current timestamp
                diagram.HeaderFooter.FooterRight = $"Generated on {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

                // Build the output file path (preserve original file name)
                string outputPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(filePath) + ".vsdx");

                // Save the diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                // Log any errors for the current file
                Console.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
