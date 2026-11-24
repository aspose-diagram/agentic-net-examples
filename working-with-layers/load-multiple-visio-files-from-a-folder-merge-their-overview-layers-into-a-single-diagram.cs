using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolderPath> <outputFilePath>");
            return;
        }

        string inputFolder = args[0];
        string outputPath = args[1];

        // Guard: input folder must exist.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Get all Visio files in the folder (supported extensions).
        string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        if (visioFiles.Length == 0)
        {
            Console.Error.WriteLine("No files found in the specified folder.");
            return;
        }

        Diagram targetDiagram = null;

        try
        {
            // Load the first diagram as the base target diagram.
            string firstFile = null;
            foreach (var file in visioFiles)
            {
                // Consider only Visio extensions.
                string ext = Path.GetExtension(file).ToLowerInvariant();
                if (ext == ".vsdx" || ext == ".vsd" || ext == ".vsx" || ext == ".vtx" || ext == ".vdx")
                {
                    firstFile = file;
                    break;
                }
            }

            if (firstFile == null)
            {
                Console.Error.WriteLine("No Visio files found in the folder.");
                return;
            }

            if (!File.Exists(firstFile))
            {
                Console.Error.WriteLine($"File not found: {firstFile}");
                return;
            }

            // Load the first diagram.
            using (Diagram firstDiagram = new Diagram(firstFile))
            {
                // Clone the first diagram into targetDiagram (create a new instance).
                // Since Diagram does not provide a direct clone, we use Combine with an empty diagram.
                targetDiagram = new Diagram();
                targetDiagram.Combine(firstDiagram);
            }

            // Process remaining files.
            foreach (var filePath in visioFiles)
            {
                if (filePath.Equals(firstFile, StringComparison.OrdinalIgnoreCase))
                    continue; // Skip the first file already loaded.

                string ext = Path.GetExtension(filePath).ToLowerInvariant();
                if (ext != ".vsdx" && ext != ".vsd" && ext != ".vsx" && ext != ".vtx" && ext != ".vdx")
                    continue; // Skip non-Visio files.

                if (!File.Exists(filePath))
                {
                    Console.Error.WriteLine($"File not found: {filePath}");
                    continue;
                }

                try
                {
                    // Load source diagram.
                    using (Diagram srcDiagram = new Diagram(filePath))
                    {
                        // Combine source diagram into the target diagram.
                        targetDiagram.Combine(srcDiagram);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            // After combining all diagrams, set visibility: only layers named "Overview" stay visible.
            foreach (Page page in targetDiagram.Pages)
            {
                // Iterate through layers on the page's PageSheet.
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name.Value.Equals("Overview", StringComparison.OrdinalIgnoreCase))
                    {
                        // Ensure the Overview layer is visible.
                        layer.Visible.Value = BOOL.True;
                    }
                    else
                    {
                        // Hide all other layers.
                        layer.Visible.Value = BOOL.False;
                    }
                }
            }

            // Save the merged diagram.
            targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Merged diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
        finally
        {
            // Dispose the target diagram if it was created.
            if (targetDiagram != null)
                targetDiagram.Dispose();
        }
    }
}