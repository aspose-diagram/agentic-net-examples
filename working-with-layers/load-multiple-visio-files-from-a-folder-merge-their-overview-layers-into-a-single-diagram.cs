using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder containing Visio files; default to current directory.
        string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
        // Guard: ensure the folder exists before proceeding.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Retrieve all VSDX files in the specified folder.
        string[] visioFiles = Directory.GetFiles(folderPath, "*.vsdx");
        // Guard: ensure at least one Visio file is present.
        if (visioFiles.Length == 0)
        {
            Console.Error.WriteLine($"No Visio files found in folder: {folderPath}");
            return;
        }

        try
        {
            // Load the first diagram as the target (base) diagram.
            Diagram targetDiagram = new Diagram(visioFiles[0]);

            // Merge each subsequent diagram into the target diagram.
            for (int i = 1; i < visioFiles.Length; i++)
            {
                // Load source diagram (Diagram does not implement IDisposable, so no using block).
                Diagram srcDiagram = new Diagram(visioFiles[i]);
                // Combine source diagram into the target diagram.
                targetDiagram.Combine(srcDiagram);
                // Allow GC to collect srcDiagram after use.
            }

            // After merging, set visibility: only layers named "Overview" stay visible.
            foreach (Page page in targetDiagram.Pages)
            {
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Compare layer name case‑insensitively.
                    if (layer.Name.Value.Equals("Overview", StringComparison.OrdinalIgnoreCase))
                    {
                        layer.Visible.Value = BOOL.True;   // Show Overview layers.
                    }
                    else
                    {
                        layer.Visible.Value = BOOL.False; // Hide all other layers.
                    }
                }
            }

            // Save the merged diagram back to the folder.
            string outputPath = Path.Combine(folderPath, "MergedOverview.vsdx");
            targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Merged diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagrams: {ex.Message}");
        }
    }
}