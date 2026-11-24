using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <folderPath>");
            return;
        }

        string folderPath = args[0];

        // Guard: ensure the specified folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Collect Visio diagram files with common extensions.
        string[] extensions = new[] { "*.vsdx", "*.vsd", "*.vdx", "*.vssx", "*.vstx", "*.vss", "*.vst" };
        var diagramFiles = new List<string>();
        foreach (string ext in extensions)
        {
            diagramFiles.AddRange(Directory.GetFiles(folderPath, ext, SearchOption.TopDirectoryOnly));
        }

        if (diagramFiles.Count == 0)
        {
            Console.WriteLine("No diagram files found in the specified folder.");
            return;
        }

        // Process each diagram file.
        foreach (string filePath in diagramFiles)
        {
            // Guard: ensure the file still exists before processing.
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
                    // Set the right footer to display the page number placeholder.
                    diagram.HeaderFooter.FooterRight = "Page &p";

                    // Optionally adjust the footer margin (in inches).
                    diagram.HeaderFooter.FooterMargin.Value = 0.5;

                    // Determine the appropriate SaveFileFormat based on the file extension.
                    SaveFileFormat format = GetSaveFormat(Path.GetExtension(filePath));

                    // Save the diagram back to its original file.
                    diagram.Save(filePath, format);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }

    // Maps file extensions to the corresponding SaveFileFormat enum values.
    static SaveFileFormat GetSaveFormat(string extension)
    {
        switch (extension.ToLowerInvariant())
        {
            case ".vsdx":
                return SaveFileFormat.Vsdx;
            case ".vsd":
                return SaveFileFormat.Vsd;
            case ".vdx":
                return SaveFileFormat.Vdx;
            case ".vssx":
                return SaveFileFormat.Vssx;
            case ".vstx":
                return SaveFileFormat.Vstx;
            case ".vss":
                return SaveFileFormat.Vss;
            case ".vst":
                return SaveFileFormat.Vst;
            default:
                // Default to Vsdx if the extension is unrecognized.
                return SaveFileFormat.Vsdx;
        }
    }
}