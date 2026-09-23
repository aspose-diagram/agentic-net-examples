using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Vba;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file (must be a macro‑enabled format)
                string sourcePath = "input.vsdm";

                // Load the diagram
                Diagram diagram = new Diagram(sourcePath);

                // Ensure the diagram contains a VBA project
                if (diagram.VbaProject == null)
                {
                    Console.WriteLine("The diagram does not contain a VBA project.");
                    return;
                }

                // Rename the desired VBA module.
                // Example: rename a module named "Module1" to "DescriptiveModule"
                bool renamed = false;
                foreach (VbaModule module in diagram.VbaProject.Modules)
                {
                    if (module.Name == "Module1")
                    {
                        module.Name = "DescriptiveModule";
                        renamed = true;
                        Console.WriteLine($"Module renamed to '{module.Name}'.");
                        break;
                    }
                }

                if (!renamed)
                {
                    Console.WriteLine("Target module not found. No changes were made.");
                }

                // Save the updated diagram in a macro‑enabled format to preserve VBA changes
                string outputPath = "output.vsdm";
                diagram.Save(outputPath, SaveFileFormat.Vsdm);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }