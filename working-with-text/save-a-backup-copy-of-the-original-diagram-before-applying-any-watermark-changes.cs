using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and backup file paths.
        string inputPath;
        string backupPath;

        if (args.Length >= 2)
        {
            inputPath = args[0];
            backupPath = args[1];
        }
        else
        {
            Console.WriteLine("Usage: Program <inputDiagramPath> <backupDiagramPath>");
            return;
        }

        // Guard: ensure the source diagram exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the original diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Save a backup copy before any modifications.
                diagram.Save(backupPath, SaveFileFormat.Vsdx);

                // TODO: Apply watermark changes to the diagram.
                // The specific API for adding a watermark is not defined in the provided reference.
                // Implement the watermark logic here using Aspose.Diagram APIs when available.

                // Example placeholder for saving the modified diagram (overwrite original or new file).
                // diagram.Save(inputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}