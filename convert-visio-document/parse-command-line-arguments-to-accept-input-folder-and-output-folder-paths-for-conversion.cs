using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Entry point of the console application.
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <exe> <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Ensure the input folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Create the output folder if it does not exist.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Optional: set font folders for Aspose.Diagram if custom fonts are required.
        // FontConfigs.SetFontFolders(new string[] { @"C:\Windows\Fonts" }, true);

        // Process each Visio file in the input folder.
        string[] visioExtensions = new[] { ".vsd", ".vsdx", ".vss", ".vssx", ".vst", ".vstx" };
        foreach (string filePath in Directory.GetFiles(inputFolder))
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (Array.IndexOf(visioExtensions, ext) < 0)
                continue; // Skip non‑Visio files.

            try
            {
                // Load the diagram using default LoadOptions.
                LoadOptions loadOptions = new LoadOptions(); // default format is VSD
                Diagram diagram = new Diagram(filePath, loadOptions);

                // Determine output file name (same name, .vdx extension).
                string outputFileName = Path.GetFileNameWithoutExtension(filePath) + ".vdx";
                string outputPath = Path.Combine(outputFolder, outputFileName);

                // Save the diagram in VDX format.
                diagram.Save(outputPath, SaveFileFormat.Vdx);
                Console.WriteLine($"Converted: {Path.GetFileName(filePath)} -> {outputFileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}
