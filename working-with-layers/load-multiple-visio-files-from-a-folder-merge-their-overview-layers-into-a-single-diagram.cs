using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input folder argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the folder path containing Visio files as the first argument.");
            return;
        }

        string folderPath = args[0];
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Gather Visio files (common extensions)
        string[] visioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        visioFiles = Array.FindAll(visioFiles, f =>
        {
            string ext = Path.GetExtension(f).ToLowerInvariant();
            return ext == ".vsdx" || ext == ".vsd" || ext == ".vsx" || ext == ".vdx";
        });

        if (visioFiles.Length == 0)
        {
            Console.Error.WriteLine("No Visio files found in the specified folder.");
            return;
        }

        Diagram mergedDiagram = null;

        try
        {
            // Load the first diagram as the base
            string firstFile = visioFiles[0];
            if (!File.Exists(firstFile))
            {
                Console.Error.WriteLine($"File not found: {firstFile}");
                return;
            }

            mergedDiagram = new Diagram(firstFile);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading the first diagram: {ex.Message}");
            return;
        }

        // Combine remaining diagrams
        for (int i = 1; i < visioFiles.Length; i++)
        {
            string filePath = visioFiles[i];
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                Diagram sourceDiagram = new Diagram(filePath);
                // Combine source into the merged diagram
                mergedDiagram.Combine(sourceDiagram);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error combining file '{filePath}': {ex.Message}");
            }
        }

        // Hide all layers except those named "Overview"
        try
        {
            foreach (Page page in mergedDiagram.Pages)
            {
                // Iterate through layers on the page sheet
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Compare layer name (case-insensitive)
                    if (!string.Equals(layer.Name.Value, "Overview", StringComparison.OrdinalIgnoreCase))
                    {
                        // Hide non-Overview layers
                        layer.Visible.Value = BOOL.False;
                    }
                    else
                    {
                        // Ensure Overview layers are visible
                        layer.Visible.Value = BOOL.True;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing layers: {ex.Message}");
            return;
        }

        // Define output path
        string outputPath = Path.Combine(folderPath, "MergedOverview.vsdx");

        // Save the merged diagram
        try
        {
            mergedDiagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Merged diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving merged diagram: {ex.Message}");
        }
    }
}