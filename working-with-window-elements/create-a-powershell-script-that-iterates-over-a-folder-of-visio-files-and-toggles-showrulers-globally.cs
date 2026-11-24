using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the target folder either from command‑line arguments or user input.
        string folderPath;
        if (args.Length > 0)
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter the folder path containing Visio files: ");
            folderPath = Console.ReadLine()?.Trim() ?? string.Empty;
        }

        // Guard: ensure the folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Define Visio file extensions we will process.
        string[] visioExtensions = new[]
        {
            ".vsdx", ".vsd", ".vsx", ".vtx",
            ".vssx", ".vss", ".vstx", ".vst",
            ".vssm", ".vsdm", ".vstm"
        };

        // Retrieve all matching files (non‑recursive).
        string[] files = Directory.GetFiles(folderPath);
        foreach (string filePath in files)
        {
            // Skip files that do not have a supported Visio extension.
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (Array.IndexOf(visioExtensions, ext) < 0)
                continue;

            // Guard: ensure the file still exists (it could have been removed meanwhile).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(filePath);

                // Ensure there is at least one window; if not, create a default drawing window.
                if (diagram.Windows.Count == 0)
                {
                    Window defaultWindow = new Window();
                    defaultWindow.WindowType = WindowTypeValue.Drawing;
                    defaultWindow.WindowState = WindowStateValue.Maximized;
                    diagram.Windows.Add(defaultWindow);
                }

                // Toggle the ShowRulers property on the first window (global setting).
                Window window = diagram.Windows[0];
                window.ShowRulers = (window.ShowRulers == BOOL.True) ? BOOL.False : BOOL.True;

                // Determine the appropriate SaveFileFormat based on the file extension.
                SaveFileFormat format = GetSaveFileFormat(ext);
                // Save the modified diagram back to the original file.
                diagram.Save(filePath, format);

                Console.WriteLine($"Processed: {Path.GetFileName(filePath)} (ShowRulers toggled)");
            }
            catch (Exception ex)
            {
                // Write any Aspose or I/O errors to the error stream.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }

    // Maps a file extension to the corresponding SaveFileFormat enum value.
    private static SaveFileFormat GetSaveFileFormat(string extension)
    {
        switch (extension)
        {
            case ".vsdx": return SaveFileFormat.Vsdx;
            case ".vsd":  return SaveFileFormat.Vsd;
            case ".vsx":  return SaveFileFormat.Vsx;
            case ".vtx":  return SaveFileFormat.Vtx;
            case ".vssx": return SaveFileFormat.Vssx;
            case ".vss":  return SaveFileFormat.Vss;
            case ".vstx": return SaveFileFormat.Vstx;
            case ".vst":  return SaveFileFormat.Vst;
            case ".vssm": return SaveFileFormat.Vssm;
            case ".vsdm": return SaveFileFormat.Vsdm;
            case ".vstm": return SaveFileFormat.Vstm;
            default:
                // Default to Vsdx if somehow an unexpected extension slips through.
                return SaveFileFormat.Vsdx;
        }
    }
}