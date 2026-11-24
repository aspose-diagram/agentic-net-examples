using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: target diagram path, source diagram path, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <targetDiagramPath> <sourceDiagramPath> <outputDiagramPath>");
            return;
        }

        string targetPath = args[0];
        string sourcePath = args[1];
        string outputPath = args[2];

        // Guard against missing input files
        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPath}");
            return;
        }
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        try
        {
            // Load the target diagram inside a using block
            using (Diagram targetDiagram = new Diagram(targetPath))
            {
                // Load the source diagram inside a using block
                using (Diagram sourceDiagram = new Diagram(sourcePath))
                {
                    // Merge source diagram into target diagram
                    targetDiagram.Combine(sourceDiagram);
                }

                // Rename all pages sequentially to avoid naming conflicts
                int pageNumber = 1;
                foreach (Page page in targetDiagram.Pages)
                {
                    // Set a new name like "Page1", "Page2", etc.
                    page.Name = $"Page{pageNumber}";
                    pageNumber++;
                }

                // Save the merged diagram with renamed pages
                targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Merged diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during Aspose operations
            Console.Error.WriteLine($"Error processing diagrams: {ex.Message}");
        }
    }
}