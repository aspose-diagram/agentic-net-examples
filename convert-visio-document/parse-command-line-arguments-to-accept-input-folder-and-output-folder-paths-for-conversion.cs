using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect exactly two arguments: input folder and output folder
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: DiagramConverter <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Validate input folder
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create output folder: {outputFolder}. Error: {ex.Message}");
                return;
            }
        }

        // Optional: set custom font folders if required
        // FontConfigs.SetFontFolders(new[] { @"C:\Windows\Fonts" }, true);

        // Process each Visio file in the input folder
        string[] visioFiles = Directory.GetFiles(inputFolder, "*.vsd");
        foreach (string filePath in visioFiles)
        {
            try
            {
                // Load the diagram using default load options
                Diagram diagram = new Diagram(filePath, new LoadOptions());

                // Build output file path (same name with .pdf extension)
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                string outputPath = Path.Combine(outputFolder, fileNameWithoutExt + ".pdf");

                // Save the diagram as PDF
                diagram.Save(outputPath, SaveFileFormat.Pdf);

                Console.WriteLine($"Converted: {filePath} -> {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}
