using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: reference diagram and target folder
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <ReferenceDiagramPath> <TargetFolderPath>");
            return;
        }

        string referencePath = args[0];
        string targetFolder = args[1];

        // Guard: reference file must exist
        if (!File.Exists(referencePath))
        {
            Console.Error.WriteLine($"Reference diagram file not found: {referencePath}");
            return;
        }

        // Guard: target folder must exist
        if (!Directory.Exists(targetFolder))
        {
            Console.Error.WriteLine($"Target folder not found: {targetFolder}");
            return;
        }

        try
        {
            // Load the reference diagram containing the master definitions to synchronize
            Diagram referenceDiagram = new Diagram(referencePath);

            // Collect all masters from the reference diagram into a list for reuse
            var referenceMasters = new System.Collections.Generic.List<Master>();
            foreach (Master master in referenceDiagram.Masters)
            {
                referenceMasters.Add(master);
            }

            // Find all Visio files (VSDX) in the target folder
            string[] targetFiles = Directory.GetFiles(targetFolder, "*.vsdx");

            foreach (string targetPath in targetFiles)
            {
                // Guard: each target file should exist (defensive check)
                if (!File.Exists(targetPath))
                {
                    Console.Error.WriteLine($"Target file not found (skipped): {targetPath}");
                    continue;
                }

                try
                {
                    // Load the target diagram that will receive missing masters
                    Diagram targetDiagram = new Diagram(targetPath);

                    // Iterate over each master from the reference diagram
                    foreach (Master refMaster in referenceMasters)
                    {
                        // Check if the master already exists in the target diagram by name
                        bool exists = targetDiagram.Masters.IsExist(refMaster.Name);
                        if (exists)
                        {
                            // Master already present; no action needed
                            continue;
                        }

                        // Import the master from the reference diagram using the master name
                        // The overload expects (Diagram sourceDiagram, string masterName)
                        targetDiagram.AddMaster(referenceDiagram, refMaster.Name);
                    }

                    // Save the updated target diagram, overwriting the original file
                    targetDiagram.Save(targetPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Synchronized masters for: {Path.GetFileName(targetPath)}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing file '{targetPath}': {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Fatal error: {ex.Message}");
        }
    }
}