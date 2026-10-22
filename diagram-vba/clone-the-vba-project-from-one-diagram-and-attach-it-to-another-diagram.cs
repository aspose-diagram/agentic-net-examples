using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

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

        // Guard against missing files
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }
        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPath}");
            return;
        }

        try
        {
            // Load the source diagram (the one containing the VBA project to copy)
            Diagram sourceDiagram = new Diagram(sourcePath);

            // Load the target diagram (the one that will receive the VBA project)
            Diagram targetDiagram = new Diagram(targetPath);

            // Ensure both diagrams have a VBA project (VbaProject is always present but may be empty)
            var sourceVba = sourceDiagram.VbaProject;
            var targetVba = targetDiagram.VbaProject;

            // Iterate through each VBA module in the source diagram and clone it into the target diagram
            foreach (VbaModule srcModule in sourceVba.Modules)
            {
                // Add a new module to the target diagram with the same type and name
                int newIndex = targetVba.Modules.Add(srcModule.Type, srcModule.Name);

                // Retrieve the newly added module and copy the VBA code
                VbaModule newModule = targetVba.Modules[newIndex];
                newModule.Codes = srcModule.Codes;
            }

            // Optionally, copy the VBA project name (read‑only property, cannot be set)
            // The name is informational; we simply output it for verification
            Console.WriteLine($"Source VBA Project Name: {sourceVba.Name}");
            Console.WriteLine($"Target VBA Project Name (unchanged): {targetVba.Name}");

            // Save the modified target diagram to the specified output path
            targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"VBA project cloned successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}