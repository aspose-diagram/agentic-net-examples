using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two arguments: source master file and one or more target files.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: SyncMasters.exe <SourceMasterVisioFile> <TargetVisioFile1> [<TargetVisioFile2> ...]");
            return;
        }

        string sourcePath = args[0];

        // Guard: verify source file exists.
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Load the source diagram containing the reference masters.
        Diagram sourceDiagram;
        try
        {
            sourceDiagram = new Diagram(sourcePath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load source diagram '{sourcePath}': {ex.Message}");
            return;
        }

        // Iterate over each target diagram path.
        for (int i = 1; i < args.Length; i++)
        {
            string targetPath = args[i];

            // Guard: verify target file exists.
            if (!File.Exists(targetPath))
            {
                Console.Error.WriteLine($"Target file not found: {targetPath}");
                continue;
            }

            // Load the target diagram.
            Diagram targetDiagram;
            try
            {
                targetDiagram = new Diagram(targetPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to load target diagram '{targetPath}': {ex.Message}");
                continue;
            }

            // Synchronize masters from source to target.
            try
            {
                foreach (Master sourceMaster in sourceDiagram.Masters)
                {
                    // Check if the master already exists in the target diagram by name.
                    bool exists = targetDiagram.Masters.IsExist(sourceMaster.Name);
                    if (!exists)
                    {
                        // Import the master from the source diagram into the target diagram.
                        // Overload: AddMaster(Diagram sourceDiagram, string masterName)
                        targetDiagram.AddMaster(sourceDiagram, sourceMaster.Name);
                        Console.WriteLine($"Added master '{sourceMaster.Name}' to '{targetPath}'.");
                    }
                    else
                    {
                        // Master exists – acknowledge its presence.
                        Console.WriteLine($"Master '{sourceMaster.Name}' already exists in '{targetPath}'.");
                    }
                }

                // Save the updated target diagram, overwriting the original file.
                targetDiagram.Save(targetPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Successfully synchronized masters and saved '{targetPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{targetPath}': {ex.Message}");
            }
        }
    }
}