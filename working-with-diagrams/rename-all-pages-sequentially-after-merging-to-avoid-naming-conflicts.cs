using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments count
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
            // Load the target diagram inside a using block as required
            using (Diagram targetDiagram = new Diagram(targetPath))
            {
                // Load the source diagram to be merged
                using (Diagram sourceDiagram = new Diagram(sourcePath))
                {
                    // Merge all pages, masters, and styles from source into target
                    targetDiagram.Combine(sourceDiagram);
                }

                // Rename all pages sequentially to avoid naming conflicts
                for (int i = 0; i < targetDiagram.Pages.Count; i++)
                {
                    // Explicitly type the page variable
                    Page page = targetDiagram.Pages[i];

                    // Construct a new sequential name (e.g., Page1, Page2, ...)
                    string newName = $"Page{i + 1}";

                    // Set both localized and universal names
                    page.Name = newName;
                    page.NameU = newName;
                }

                // Save the merged diagram with renamed pages
                targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram merged and pages renamed successfully.");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}