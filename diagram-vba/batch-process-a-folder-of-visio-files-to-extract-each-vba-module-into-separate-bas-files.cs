using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class VbaExtractor
{
    // Entry point: args[0] = input folder, args[1] = output folder
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: VbaExtractor <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process all Visio files in the input folder (common extensions)
        string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in visioFiles)
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".vsd" && ext != ".vsdx" && ext != ".vsdm")
                continue; // skip non-Visio files

            // Load diagram using the provided constructor (lifecycle rule)
            using (Diagram diagram = new Diagram(filePath))
            {
                // Check if the diagram contains a VBA project
                VbaProject vbaProject = diagram.VbaProject;
                if (vbaProject == null)
                {
                    Console.WriteLine($"No VBA project found in: {Path.GetFileName(filePath)}");
                    continue;
                }

                // Iterate through each VBA module
                foreach (VbaModule module in vbaProject.Modules)
                {
                    // Build a unique file name for the module
                    string baseFileName = $"{Path.GetFileNameWithoutExtension(filePath)}_{module.Name}.bas";
                    string outputPath = Path.Combine(outputFolder, baseFileName);

                    // Write the module's code to a .bas file
                    File.WriteAllText(outputPath, module.Codes ?? string.Empty);
                    Console.WriteLine($"Extracted module '{module.Name}' to: {outputPath}");
                }
            }
        }

        Console.WriteLine("VBA extraction completed.");
    }
}
