using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioBatchCleaner
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the folder containing Visio files
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide the path to the folder containing Visio files.");
            return;
        }

        string folderPath = args[0];
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Visio file extensions to process
        string[] visioExtensions = new[] { ".vsdx", ".vsd", ".vdx", ".vssx", ".vss", ".vstx", ".vst" };

        foreach (string filePath in Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories))
        {
            if (Array.IndexOf(visioExtensions, Path.GetExtension(filePath).ToLower()) < 0)
                continue; // Skip non‑Visio files

            try
            {
                // Load the diagram using the constructor that accepts a file path
                Diagram diagram = new Diagram(filePath);

                // Remove unused masters, styles, and other hidden information
                diagram.RemoveHiddenInformation(0);

                // Remove any VBA/macros that might be present
                diagram.RemoveMacro();

                // Detect the original file format so we can save back in the same format
                var formatInfo = FileFormatUtil.DetectFileFormat(filePath);

                // Save the cleaned diagram, preserving the original format
                diagram.Save(filePath, (SaveFileFormat)formatInfo.FileFormatType);

                // Release resources
                diagram.Dispose();

                Console.WriteLine($"Successfully processed: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }
    }
}
