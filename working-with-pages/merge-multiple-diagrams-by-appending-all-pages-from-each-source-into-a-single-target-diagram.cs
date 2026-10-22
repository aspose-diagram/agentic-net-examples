using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: first argument is output file, remaining are source diagrams.
        if (args == null || args.Length < 2)
        {
            Console.Error.WriteLine("Usage: MergeDiagrams <output.vsdx> <source1.vsdx> [<source2.vsdx> ...]");
            return;
        }

        string outputPath = args[0];

        // Verify that each source file exists before proceeding.
        for (int i = 1; i < args.Length; i++)
        {
            string srcPath = args[i];
            if (!File.Exists(srcPath))
            {
                Console.Error.WriteLine($"Source file does not exist: {srcPath}");
                return;
            }
        }

        // Load the first diagram as the base target diagram.
        string firstSource = args[1];
        try
        {
            using (Diagram targetDiagram = new Diagram(firstSource))
            {
                // Append pages from each additional source diagram.
                for (int i = 2; i < args.Length; i++)
                {
                    string srcPath = args[i];
                    try
                    {
                        using (Diagram sourceDiagram = new Diagram(srcPath))
                        {
                            // Combine merges masters, styles, and all pages from source into target.
                            targetDiagram.Combine(sourceDiagram);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Error combining '{srcPath}': {ex.Message}");
                        // Continue with remaining files.
                    }
                }

                // Save the merged diagram to the specified output path in VSDX format.
                try
                {
                    targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Merged diagram saved to: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error saving merged diagram: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading base diagram '{firstSource}': {ex.Message}");
        }
    }
}