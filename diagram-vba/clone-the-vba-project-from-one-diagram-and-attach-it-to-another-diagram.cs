using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: source diagram path, target diagram path, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <sourceDiagramPath> <targetDiagramPath> <outputDiagramPath>");
            return;
        }

        string sourcePath = args[0];
        string targetPath = args[1];
        string outputPath = args[2];

        // Guard: ensure source file exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Guard: ensure target file exists
        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPath}");
            return;
        }

        try
        {
            // Load source diagram (contains the VBA project to clone)
            using (Diagram sourceDiagram = new Diagram(sourcePath))
            // Load target diagram (will receive the cloned VBA project)
            using (Diagram targetDiagram = new Diagram(targetPath))
            {
                // Verify that the source diagram has a VBA project
                if (sourceDiagram.VbaProject == null)
                {
                    Console.Error.WriteLine("Source diagram does not contain a VBA project.");
                    return;
                }

                // Ensure the target diagram has a VBA project (create if null)
                if (targetDiagram.VbaProject == null)
                {
                    Console.Error.WriteLine("Target diagram does not contain a VBA project. Unable to attach VBA modules.");
                    return;
                }

                // Iterate through each VBA module in the source diagram
                foreach (VbaModule srcModule in sourceDiagram.VbaProject.Modules)
                {
                    // Add a new module to the target diagram with the same type and name
                    int newIndex = targetDiagram.VbaProject.Modules.Add(srcModule.Type, srcModule.Name);

                    // Retrieve the newly added module and copy the VBA code
                    VbaModule tgtModule = targetDiagram.VbaProject.Modules[newIndex];
                    tgtModule.Codes = srcModule.Codes;
                }

                // Optionally copy the VBA project name (read-only property, so we only display it)
                Console.WriteLine($"Cloned VBA project '{sourceDiagram.VbaProject.Name}' to target diagram.");

                // Save the modified target diagram to the specified output path
                targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Target diagram saved with cloned VBA project to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}