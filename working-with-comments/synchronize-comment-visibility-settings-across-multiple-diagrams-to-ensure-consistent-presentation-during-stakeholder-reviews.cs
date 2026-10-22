using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: first argument should be "show" or "hide", second argument is the folder path containing diagrams.
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: Program <show|hide> <diagrams_folder_path>");
            return;
        }

        string visibilityArg = args[0].Trim().ToLowerInvariant();
        string folderPath = args[1];

        // Determine whether comments should be exported.
        bool exportComments;
        if (visibilityArg == "show")
            exportComments = true;
        else if (visibilityArg == "hide")
            exportComments = false;
        else
        {
            Console.Error.WriteLine("First argument must be 'show' or 'hide'.");
            return;
        }

        // Verify the folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Get all Visio diagram files in the folder (common extensions).
        string[] diagramFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in diagramFiles)
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".vsdx" && ext != ".vsd" && ext != ".vdx" && ext != ".vssx" && ext != ".vss" && ext != ".vstx" && ext != ".vst")
                continue; // Skip non-diagram files.

            // Guard: ensure the file exists.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Prepare image export options with the desired comment visibility.
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    saveOptions.IsExportComments = exportComments;

                    // Build output file name (same name with _export.png suffix).
                    string outputFileName = Path.GetFileNameWithoutExtension(filePath) + "_export.png";
                    string outputPath = Path.Combine(folderPath, outputFileName);

                    // Save the diagram as PNG with the configured options.
                    diagram.Save(outputPath, saveOptions);

                    // Prepare a readable status message.
                    string commentStatus = exportComments ? "shown" : "hidden";
                    Console.WriteLine($"Processed '{Path.GetFileName(filePath)}' -> '{outputFileName}' (Comments {commentStatus})");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}
