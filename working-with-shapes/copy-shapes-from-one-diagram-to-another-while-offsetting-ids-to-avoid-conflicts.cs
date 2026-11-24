using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: source diagram path, target diagram path, output diagram path.
        string sourcePath = args.Length > 0 ? args[0] : "source.vsdx";
        string targetPath = args.Length > 1 ? args[1] : "target.vsdx";
        string outputPath = args.Length > 2 ? args[2] : "merged.vsdx";

        // Guard clauses to ensure input files exist.
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
            // Load the source diagram inside a using block.
            using (Diagram sourceDiagram = new Diagram(sourcePath))
            {
                // Load the target diagram inside a using block.
                using (Diagram targetDiagram = new Diagram(targetPath))
                {
                    // Combine the source diagram into the target diagram.
                    // This merges pages, masters, and shapes while handling ID conflicts internally.
                    targetDiagram.Combine(sourceDiagram);

                    // Save the merged diagram to the specified output path.
                    targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
            }

            Console.WriteLine($"Shapes copied successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}