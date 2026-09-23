using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
    {
        static void Main(string[] args)
        {
            // Determine input and output folders
            string inputFolder;
            string outputFolder;

            if (args.Length >= 2)
            {
                inputFolder = args[0];
                outputFolder = args[1];
            }
            else
            {
                Console.Write("Enter the path to the folder containing Visio files: ");
                inputFolder = Console.ReadLine()?.Trim() ?? string.Empty;

                Console.Write("Enter the path to the folder where VBA modules will be saved: ");
                outputFolder = Console.ReadLine()?.Trim() ?? string.Empty;
            }

            if (!Directory.Exists(inputFolder))
            {
                Console.WriteLine($"Input folder does not exist: {inputFolder}");
                return;
            }

            // Ensure the output folder exists
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Supported Visio file extensions
            string[] extensions = new[] { ".vsdx", ".vsdm", ".vsd", ".vdx", ".vsx", ".vtx", ".vssx", ".vssm", ".vstx", ".vstm", ".vss", ".vst" };

            // Get all Visio files in the input folder (non-recursive)
            string[] files = Directory.GetFiles(inputFolder);
            foreach (string filePath in files)
            {
                string ext = Path.GetExtension(filePath);
                if (Array.IndexOf(extensions, ext, 0, extensions.Length) < 0)
                {
                    // Skip non-Visio files
                    continue;
                }

                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(filePath);

                    // Check if the diagram contains a VBA project
                    if (diagram.VbaProject == null)
                    {
                        Console.WriteLine($"No VBA project found in file: {Path.GetFileName(filePath)}");
                        continue;
                    }

                    // Iterate through each VBA module
                    foreach (VbaModule module in diagram.VbaProject.Modules)
                    {
                        string moduleName = module.Name;
                        string moduleCode = module.Codes ?? string.Empty;

                        // Build a unique file name for the module
                        string baseFileName = Path.GetFileNameWithoutExtension(filePath);
                        string safeModuleName = string.IsNullOrWhiteSpace(moduleName) ? "UnnamedModule" : moduleName;
                        string outputFileName = $"{baseFileName}_{safeModuleName}.bas";
                        string outputPath = Path.Combine(outputFolder, outputFileName);

                        // Write the VBA code to a .bas file
                        File.WriteAllText(outputPath, moduleCode);
                        Console.WriteLine($"Extracted module '{safeModuleName}' from '{Path.GetFileName(filePath)}' to '{outputFileName}'.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
                }
            }

            Console.WriteLine("VBA module extraction completed.");
        }
    }