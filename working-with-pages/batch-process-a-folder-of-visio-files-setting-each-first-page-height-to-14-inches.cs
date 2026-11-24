using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line argument for the target folder.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the folder containing Visio files as the first argument.");
            return;
        }

        string folderPath = args[0];

        // Guard: ensure the folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Retrieve all files with Visio extensions in the specified folder.
        string[] visioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in visioFiles)
        {
            // Filter supported Visio extensions (case‑insensitive).
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".vsdx" && ext != ".vsd" && ext != ".vsx" && ext != ".vtx" &&
                ext != ".vdx" && ext != ".vssx" && ext != ".vss" &&
                ext != ".vstx" && ext != ".vst" && ext != ".vstm")
            {
                // Skip non‑Visio files.
                continue;
            }

            // Guard: ensure the file still exists before processing.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found (skipping): {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(filePath);

                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine($"Diagram has no pages (skipping): {filePath}");
                    continue;
                }

                // Access the first page explicitly using the Page type.
                Page firstPage = diagram.Pages[0];

                // Set the first page height to 14 inches.
                firstPage.PageSheet.PageProps.PageHeight.Value = 14.0;

                // Determine the appropriate save format based on the original file extension.
                SaveFileFormat format = GetSaveFormat(ext);

                // Save the modified diagram back to the original file path.
                diagram.Save(filePath, format);

                Console.WriteLine($"Processed and saved: {filePath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of the current file.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Maps a file extension to the corresponding Aspose.Diagram SaveFileFormat enum value.
    /// </summary>
    /// <param name="extension">File extension including the leading dot, in lower case.</param>
    /// <returns>SaveFileFormat matching the extension.</returns>
    private static SaveFileFormat GetSaveFormat(string extension)
    {
        // Default to Vsdx if the extension is unrecognized (should not happen due to prior filtering).
        switch (extension)
        {
            case ".vsdx":
                return SaveFileFormat.Vsdx;
            case ".vsd":
                return SaveFileFormat.Vsd;
            case ".vsx":
                return SaveFileFormat.Vsx;
            case ".vtx":
                return SaveFileFormat.Vtx;
            case ".vdx":
                return SaveFileFormat.Vdx;
            case ".vssx":
                return SaveFileFormat.Vssx;
            case ".vss":
                return SaveFileFormat.Vss;
            case ".vstx":
                return SaveFileFormat.Vstx;
            case ".vst":
                return SaveFileFormat.Vst;
            case ".vstm":
                return SaveFileFormat.Vstm;
            default:
                return SaveFileFormat.Vsdx;
        }
    }
}