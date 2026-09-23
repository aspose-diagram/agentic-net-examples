using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (must be a macro-enabled format if it contains VBA)
                string visioPath = "input.vsdm";

                // Name of the VBA module to extract
                string targetModuleName = "Module1";

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

                // Ensure the diagram contains a VBA project
                if (diagram.VbaProject == null)
                {
                    Console.WriteLine("The diagram does not contain a VBA project.");
                    return;
                }

                // Find the requested module
                VbaModule? targetModule = null;
                foreach (VbaModule module in diagram.VbaProject.Modules)
                {
                    if (string.Equals(module.Name, targetModuleName, StringComparison.OrdinalIgnoreCase))
                    {
                        targetModule = module;
                        break;
                    }
                }

                if (targetModule == null)
                {
                    Console.WriteLine($"VBA module '{targetModuleName}' not found.");
                    return;
                }

                // Extract the VBA source code
                string vbaCode = targetModule.Codes;

                // Define output .bas file path
                string outputPath = $"{targetModule.Name}.bas";

                // Write the code to the .bas file
                File.WriteAllText(outputPath, vbaCode);

                Console.WriteLine($"VBA module '{targetModule.Name}' extracted to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }