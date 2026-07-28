using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioBatchCleaner
{
    // Entry point
    static void Main(string[] args)
    {
        // Validate input folder
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide the folder path containing Visio files.");
            return;
        }

        string folderPath = args[0];
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Process each Visio file in the folder (non‑recursive)
        foreach (string filePath in Directory.GetFiles(folderPath))
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            // Consider only supported Visio extensions
            if (ext != ".vsd" && ext != ".vsdx" && ext != ".vsdm" &&
                ext != ".vss" && ext != ".vssx" && ext != ".vssm" &&
                ext != ".vst" && ext != ".vstx" && ext != ".vstm")
            {
                continue;
            }

            try
            {
                // Load the diagram using the appropriate constructor
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Remove unused masters and other hidden information
                    // Parameter 0 removes all hidden/unused data
                    diagram.RemoveHiddenInformation(0);

                    // Optional: also strip any VBA/macros that may be present
                    diagram.RemoveMacro();

                    // Determine the save format based on original extension
                    SaveFileFormat saveFormat = GetSaveFormatFromExtension(ext);

                    // Overwrite the original file with cleaned content
                    diagram.Save(filePath, saveFormat);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }
    }

    // Maps file extension to the corresponding Aspose.Diagram SaveFileFormat enum value
    private static SaveFileFormat GetSaveFormatFromExtension(string extension)
    {
        switch (extension)
        {
            case ".vsd":
                return SaveFileFormat.Vsd;
            case ".vsdx":
                return SaveFileFormat.Vsdx;
            case ".vsdm":
                return SaveFileFormat.Vsdm;
            case ".vss":
                return SaveFileFormat.Vss;
            case ".vssx":
                return SaveFileFormat.Vssx;
            case ".vssm":
                return SaveFileFormat.Vssm;
            case ".vst":
                return SaveFileFormat.Vst;
            case ".vstx":
                return SaveFileFormat.Vstx;
            case ".vstm":
                return SaveFileFormat.Vstm;
            default:
                // Default to Vsd if unknown (should not happen due to earlier filter)
                return SaveFileFormat.Vsd;
        }
    }
}
