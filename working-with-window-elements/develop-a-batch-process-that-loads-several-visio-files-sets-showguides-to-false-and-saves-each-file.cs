using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <folderPath>");
            return;
        }

        string folderPath = args[0];

        // Guard: ensure the folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Get all Visio files in the folder (common extensions).
        string[] visioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in visioFiles)
        {
            // Process only supported Visio extensions.
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".vsdx" && ext != ".vsd" && ext != ".vsx" && ext != ".vtx" &&
                ext != ".vssx" && ext != ".vstx" && ext != ".vsdm" && ext != ".vssm" &&
                ext != ".vstm" && ext != ".vdw" && ext != ".vss" && ext != ".vst" &&
                ext != ".mmd" && ext != ".html")
            {
                continue;
            }

            // Guard: ensure the file exists (should always be true here).
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
                    // Iterate through all windows and disable guide visibility.
                    foreach (Window window in diagram.Windows)
                    {
                        window.ShowGuides = BOOL.False;
                    }

                    // Save the diagram back to the same file in VSDX format.
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"Processed successfully: {filePath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}