using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class VbaExtractor
{
    static void Main(string[] args)
    {
        // Expect the folder path as the first argument
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: VbaExtractor <folderPath>");
            return;
        }

        string folderPath = args[0];
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Root folder where extracted modules will be saved
        string outputRoot = Path.Combine(folderPath, "VbaModules");
        Directory.CreateDirectory(outputRoot);

        // Visio file extensions to process
        string[] extensions = new[] { ".vsd", ".vsdx", ".vsdm", ".vss", ".vssx", ".vssm", ".vst", ".vstx", ".vstm" };

        foreach (string filePath in Directory.EnumerateFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly))
        {
            if (Array.IndexOf(extensions, Path.GetExtension(filePath).ToLower()) < 0)
                continue; // Skip non‑Visio files

            // Load the Visio diagram (uses the Diagram(string) constructor)
            using (Diagram diagram = new Diagram(filePath))
            {
                VbaProject vbaProject = diagram.VbaProject;
                if (vbaProject == null)
                {
                    Console.WriteLine($"No VBA project found in {Path.GetFileName(filePath)}");
                    continue;
                }

                // Create a subfolder for this diagram's modules
                string diagramFolder = Path.Combine(outputRoot, Path.GetFileNameWithoutExtension(filePath));
                Directory.CreateDirectory(diagramFolder);

                // Extract each VBA module
                foreach (VbaModule module in vbaProject.Modules)
                {
                    string moduleName = module.Name;
                    string moduleCode = module.Codes ?? string.Empty;

                    // Ensure a valid file name
                    string safeName = string.Concat(moduleName.Split(Path.GetInvalidFileNameChars()));
                    string outFile = Path.Combine(diagramFolder, safeName + ".bas");

                    File.WriteAllText(outFile, moduleCode);
                    Console.WriteLine($"Extracted module '{moduleName}' to {outFile}");
                }
            }
        }
    }
}
