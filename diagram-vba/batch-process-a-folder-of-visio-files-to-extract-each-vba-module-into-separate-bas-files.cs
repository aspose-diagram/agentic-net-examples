using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class VbaExtractor
{
    // Processes all Visio files in the specified input folder
    // and extracts each VBA module to a separate .bas file in the output folder.
    public static void ProcessFolder(string inputFolder, string outputFolder)
    {
        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get Visio files (common extensions)
        string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in visioFiles)
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".vsd" && ext != ".vsdx" && ext != ".vsdm")
                continue; // Skip non‑Visio files

            // Load the diagram using the Diagram(string) constructor (lifecycle rule)
            using (Diagram diagram = new Diagram(filePath))
            {
                // Access the VBA project; if none, skip
                VbaProject vbaProject = diagram.VbaProject;
                if (vbaProject == null)
                    continue;

                // Iterate through all VBA modules
                foreach (VbaModule module in vbaProject.Modules)
                {
                    // Build a unique file name for the module
                    string baseFileName = $"{Path.GetFileNameWithoutExtension(filePath)}_{module.Name}.bas";
                    string outputPath = Path.Combine(outputFolder, baseFileName);

                    // Write the module's code to the .bas file
                    File.WriteAllText(outputPath, module.Codes ?? string.Empty);
                }
            }
        }
    }

    // Example usage
    static void Main(string[] args)
    {
        // Input folder containing Visio files
        string inputFolder = @"C:\VisioFiles";

        // Output folder where .bas files will be saved
        string outputFolder = @"C:\VbaModules";

        ProcessFolder(inputFolder, outputFolder);
        Console.WriteLine("VBA modules extraction completed.");
    }
}
