using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Prompt for folder path if not provided as an argument
        string folderPath = args.Length > 0 ? args[0] : "";
        if (string.IsNullOrWhiteSpace(folderPath))
        {
            Console.Write("Enter the folder path containing Visio files: ");
            folderPath = Console.ReadLine() ?? "";
        }

        // Guard: ensure the folder exists before proceeding
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Retrieve all Visio files with common extensions in the specified folder
        string[] visioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in visioFiles)
        {
            // Guard: verify each file still exists (defensive check)
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Process only supported Visio extensions
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (!IsSupportedExtension(ext))
            {
                Console.WriteLine($"Skipping unsupported file: {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram from the file
                Diagram diagram = new Diagram(filePath);

                // Ensure the diagram has at least one window; if not, create a default one
                if (diagram.Windows.Count == 0)
                {
                    Window defaultWindow = new Window
                    {
                        WindowType = WindowTypeValue.Drawing,
                        WindowState = WindowStateValue.Maximized,
                        WindowWidth = 1100,
                        WindowHeight = 700,
                        ShowRulers = BOOL.True // initial value; will be toggled below
                    };
                    diagram.Windows.Add(defaultWindow);
                }

                // Toggle ShowRulers for each window (global effect)
                foreach (Window window in diagram.Windows)
                {
                    // If rulers are currently shown, hide them; otherwise, show them
                    window.ShowRulers = window.ShowRulers == BOOL.True ? BOOL.False : BOOL.True;
                }

                // Determine the appropriate SaveFileFormat based on the original extension
                SaveFileFormat saveFormat = GetSaveFileFormat(ext);

                // Overwrite the original file with the updated diagram
                diagram.Save(filePath, saveFormat);

                Console.WriteLine($"Toggled ShowRulers for: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                // Report any errors encountered while processing the file
                Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }
    }

    // Helper: checks if the file extension is a supported Visio format
    private static bool IsSupportedExtension(string extension)
    {
        return extension switch
        {
            ".vsdx" => true,
            ".vsd"  => true,
            ".vdx"  => true,
            ".vsx"  => true,
            ".vtx"  => true,
            ".vssx" => true,
            ".vstx" => true,
            ".vsdm" => true,
            ".vssm" => true,
            ".vstm" => true,
            ".vss"  => true,
            ".vst"  => true,
            _ => false,
        };
    }

    // Helper: maps a file extension to the corresponding SaveFileFormat enum value
    private static SaveFileFormat GetSaveFileFormat(string extension)
    {
        return extension switch
        {
            ".vsdx" => SaveFileFormat.Vsdx,
            ".vsd"  => SaveFileFormat.Vsd,
            ".vdx"  => SaveFileFormat.Vdx,
            ".vsx"  => SaveFileFormat.Vsx,
            ".vtx"  => SaveFileFormat.Vtx,
            ".vssx" => SaveFileFormat.Vssx,
            ".vstx" => SaveFileFormat.Vstx,
            ".vsdm" => SaveFileFormat.Vsdm,
            ".vssm" => SaveFileFormat.Vssm,
            ".vstm" => SaveFileFormat.Vstm,
            ".vss"  => SaveFileFormat.Vss,
            ".vst"  => SaveFileFormat.Vst,
            // Default fallback (should not occur due to prior filtering)
            _ => SaveFileFormat.Vsdx,
        };
    }
}