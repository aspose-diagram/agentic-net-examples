using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the source diagram (with VBA) and the target diagram.
                string sourceDiagramPath = "source.vsdx";
                string targetDiagramPath = "target.vsdx";
                string outputDiagramPath = "target_with_vba.vsdm";

                // Load the source diagram containing the VBA project.
                Diagram sourceDiagram = new Diagram(sourceDiagramPath);

                // Load the target diagram to which the VBA project will be attached.
                Diagram targetDiagram = new Diagram(targetDiagramPath);

                // Ensure the target diagram has a VBA project container.
                // The VbaProject property is always available; we can add modules to it.
                VbaProject targetVba = targetDiagram.VbaProject;
                VbaProject sourceVba = sourceDiagram.VbaProject;

                // Iterate through each module in the source VBA project.
                foreach (VbaModule srcModule in sourceVba.Modules)
                {
                    // Add a new module to the target VBA project with the same type and name.
                    int newIndex = targetVba.Modules.Add(srcModule.Type, srcModule.Name);

                    // Retrieve the newly added module.
                    VbaModule tgtModule = targetVba.Modules[newIndex];

                    // Copy the VBA code from the source module to the target module.
                    tgtModule.Codes = srcModule.Codes;
                }

                // Save the target diagram with the cloned VBA project.
                // Use a macro-enabled format to preserve VBA (Vsdm).
                targetDiagram.Save(outputDiagramPath, SaveFileFormat.Vsdm);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }