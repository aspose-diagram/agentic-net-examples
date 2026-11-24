using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    // Standard disclaimer text to be added as a comment.
    private const string DisclaimerText = "Disclaimer: This diagram is confidential and for internal use only.";

    static void Main(string[] args)
    {
        // Determine the target directory either from command‑line arguments or user input.
        string targetDirectory;
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            targetDirectory = args[0];
        }
        else
        {
            Console.Write("Enter the path to the project directory containing Visio diagrams: ");
            targetDirectory = Console.ReadLine();
        }

        // Guard: ensure the directory exists.
        if (string.IsNullOrWhiteSpace(targetDirectory) || !Directory.Exists(targetDirectory))
        {
            Console.Error.WriteLine("Error: The specified directory does not exist.");
            return;
        }

        // Define supported Visio file extensions.
        string[] supportedExtensions = new[] { ".vsdx", ".vsd", ".vdx", ".vssx", ".vss", ".vstx", ".vst", ".vtx" };

        // Retrieve all files with supported extensions.
        string[] diagramFiles = Directory.GetFiles(targetDirectory, "*.*", SearchOption.AllDirectories);
        foreach (string filePath in diagramFiles)
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (Array.IndexOf(supportedExtensions, extension) < 0)
                continue; // Skip unsupported files.

            // Guard: ensure the file still exists before processing.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"Warning: File not found '{filePath}'. Skipping.");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate through each page and add the disclaimer comment at (0,0).
                    for (int i = 0; i < diagram.Pages.Count; i++)
                    {
                        Page page = diagram.Pages[i];
                        // Add a page‑level comment. Coordinates (0,0) place it at the origin.
                        page.AddComment(0, 0, DisclaimerText);
                    }

                    // Determine the appropriate SaveFileFormat based on the original extension.
                    SaveFileFormat format = GetSaveFileFormat(extension);
                    if (format == SaveFileFormat.Vsd) // Default fallback if mapping fails.
                    {
                        Console.Error.WriteLine($"Warning: Unrecognized format for '{filePath}'. Saving as Vsdx.");
                        format = SaveFileFormat.Vsdx;
                    }

                    // Overwrite the original file with the updated diagram.
                    diagram.Save(filePath, format);
                    Console.WriteLine($"Processed and saved: {filePath}");
                }
            }
            catch (Exception ex)
            {
                // Capture any Aspose or IO exceptions and report them.
                Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Maps a file extension to the corresponding Aspose.Diagram SaveFileFormat enum value.
    /// </summary>
    private static SaveFileFormat GetSaveFileFormat(string extension)
    {
        switch (extension)
        {
            case ".vsdx": return SaveFileFormat.Vsdx;
            case ".vsd":  return SaveFileFormat.Vsd;
            case ".vdx":  return SaveFileFormat.Vdx;
            case ".vssx": return SaveFileFormat.Vssx;
            case ".vss":  return SaveFileFormat.Vss;
            case ".vstx": return SaveFileFormat.Vstx;
            case ".vst":  return SaveFileFormat.Vst;
            case ".vtx":  return SaveFileFormat.Vtx;
            default:      return SaveFileFormat.Vsd; // Fallback (will be overridden in caller).
        }
    }
}