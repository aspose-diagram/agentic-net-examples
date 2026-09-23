using System;
using System.IO;
using System.Linq; // Required for LINQ extension methods used below.
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder to process (from args or user input).
        string folderPath;
        if (args.Length > 0)
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter the full path to the folder containing Visio files: ");
            folderPath = Console.ReadLine()?.Trim() ?? string.Empty;
        }

        // Verify the folder exists before proceeding.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Supported Visio file extensions.
        string[] extensions = new[]
        {
            ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx",
            ".vssx", ".vss", ".vstx", ".vst", ".vstm",
            ".vssm", ".vsdm"
        };

        // Gather all Visio files in the folder (non‑recursive) using LINQ.
        var visioFiles = Directory.GetFiles(folderPath)
                                  .Where(f => extensions.Contains(Path.GetExtension(f).ToLower()))
                                  .ToArray();

        // If no files were found, inform the user and exit.
        if (visioFiles.Length == 0)
        {
            Console.WriteLine("No Visio files found in the specified folder.");
            return;
        }

        // Process each Visio file individually.
        foreach (var filePath in visioFiles)
        {
            // Guard to ensure the file actually exists before loading.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue; // Skip to the next file.
            }

            try
            {
                // Load the Visio diagram from the file.
                Diagram diagram = new Diagram(filePath);

                // Apply Landscape orientation to every page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                }

                // Determine the appropriate SaveFileFormat based on the file extension.
                SaveFileFormat format = GetSaveFileFormat(Path.GetExtension(filePath));

                // Save the modified diagram back to the original file using the same format.
                diagram.Save(filePath, format);

                Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing of the current file.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }

    // Maps file extensions to the corresponding SaveFileFormat enum value.
    private static SaveFileFormat GetSaveFileFormat(string extension)
    {
        switch (extension.ToLower())
        {
            case ".vsdx": return SaveFileFormat.Vsdx;
            case ".vsd":  return SaveFileFormat.Vsd;
            case ".vdx":  return SaveFileFormat.Vdx;
            case ".vsx":  return SaveFileFormat.Vsx;
            case ".vtx":  return SaveFileFormat.Vtx;
            case ".vssx": return SaveFileFormat.Vssx;
            case ".vss":  return SaveFileFormat.Vss;
            case ".vstx": return SaveFileFormat.Vstx;
            case ".vst":  return SaveFileFormat.Vst;
            case ".vstm": return SaveFileFormat.Vstm;
            case ".vssm": return SaveFileFormat.Vssm;
            case ".vsdm": return SaveFileFormat.Vsdm;
            default:      return SaveFileFormat.Vsdx; // Fallback to VSDX if unknown.
        }
    }
}