using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input folder: first argument or current directory
        string inputFolder = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        // Output folder: second argument or a subfolder named "output"
        string outputFolder = args.Length > 1 ? args[1] : Path.Combine(inputFolder, "output");
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        // Get all supported Visio files in the input folder
        string[] files = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in files)
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".vsdx" && ext != ".vdx" && ext != ".vsd" && ext != ".vsx" && ext != ".vtx")
                continue; // Skip unsupported files

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Update the footer with the current timestamp
                diagram.HeaderFooter.FooterRight = "Generated on: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // Save the updated diagram to the output folder, preserving the original format (VSDX)
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Processed: {filePath} -> {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }
    }
}
