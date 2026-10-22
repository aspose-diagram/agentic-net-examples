using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> [outputFolder]");
            return;
        }

        string inputFolder = args[0];
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Determine output folder (optional second argument or default subfolder)
        string outputFolder = args.Length >= 2 ? args[1] : Path.Combine(inputFolder, "Processed");
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder '{outputFolder}': {ex.Message}");
                return;
            }
        }

        // Supported Visio extensions
        string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vtx" };

        // Process each file in the input folder
        foreach (string filePath in Directory.GetFiles(inputFolder))
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (Array.IndexOf(extensions, ext) < 0)
                continue; // Skip non‑Visio files

            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram (Diagram does not implement IDisposable)
                Diagram diagram = new Diagram(filePath);

                // -----------------------------------------------------------------
                // Remove hidden information to reduce file size.
                // The RemoveHiddenInformation method requires an integer flag.
                // Passing 0 removes all hidden information (default behavior).
                // -----------------------------------------------------------------
                diagram.RemoveHiddenInformation(0);

                // Prepare save options: auto‑fit page to drawing content
                DiagramSaveOptions saveOptions = new DiagramSaveOptions(GetSaveFileFormat(ext));
                saveOptions.AutoFitPageToDrawingContent = true; // property expects a bool

                // Build the output file path
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));

                // Save the cleaned diagram
                diagram.Save(outputPath, saveOptions);

                Console.WriteLine($"Processed: {filePath} -> {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }
    }

    // Maps file extensions to the corresponding SaveFileFormat enum values
    private static SaveFileFormat GetSaveFileFormat(string extension)
    {
        switch (extension.ToLowerInvariant())
        {
            case ".vsdx":
                return SaveFileFormat.Vsdx;
            case ".vsd":
                return SaveFileFormat.Vsd;
            case ".vdx":
                return SaveFileFormat.Vdx;
            case ".vtx":
                return SaveFileFormat.Vtx;
            default:
                // Default to Vsdx (should not occur due to prior filtering)
                return SaveFileFormat.Vsdx;
        }
    }
}
