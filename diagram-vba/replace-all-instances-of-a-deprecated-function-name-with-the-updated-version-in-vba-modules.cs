using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file (must be macro-enabled to contain VBA)
                string inputPath = "input.vsdm";
                // Output Visio file after replacement
                string outputPath = "output.vsdm";

                // Deprecated function name and its replacement
                string oldFunctionName = "OldFunction";
                string newFunctionName = "NewFunction";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure the diagram contains a VBA project
                if (diagram.VbaProject == null)
                {
                    Console.WriteLine("No VBA project found in the diagram.");
                    return;
                }

                // Iterate through all VBA modules and replace the deprecated function name
                foreach (VbaModule module in diagram.VbaProject.Modules)
                {
                    if (!string.IsNullOrEmpty(module.Codes))
                    {
                        string updatedCode = module.Codes.Replace(oldFunctionName, newFunctionName);
                        module.Codes = updatedCode;
                    }
                }

                // Save the diagram in a macro-enabled format to preserve VBA changes
                diagram.Save(outputPath, SaveFileFormat.Vsdm);

                Console.WriteLine($"VBA modules updated and saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }