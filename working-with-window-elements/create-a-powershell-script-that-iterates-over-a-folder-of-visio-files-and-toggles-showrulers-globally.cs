using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Folder to process – use first argument or current directory if none provided
        string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Get all files in the folder (filter later by Visio extensions)
        string[] allFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in allFiles)
        {
            // Guard to ensure the file still exists before processing
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (!IsVisioExtension(ext))
                continue; // skip non‑Visio files

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Ensure at least one window exists (ShowRulers is a window property)
                if (diagram.Windows.Count == 0)
                {
                    Window newWindow = new Window
                    {
                        WindowType = WindowTypeValue.Drawing,
                        WindowState = WindowStateValue.Maximized,
                        WindowWidth = 1100,
                        WindowHeight = 700,
                        ShowRulers = BOOL.True // initial value
                    };
                    diagram.Windows.Add(newWindow);
                }

                // Toggle ShowRulers for each window in the diagram
                foreach (Window win in diagram.Windows)
                {
                    win.ShowRulers = (win.ShowRulers == BOOL.True) ? BOOL.False : BOOL.True;
                }

                // Determine the appropriate SaveFileFormat based on the original extension
                SaveFileFormat format = GetSaveFileFormat(ext);

                // Save the diagram back, overwriting the original file
                diagram.Save(filePath, format);

                Console.WriteLine($"Processed: {Path.GetFileName(filePath)} – ShowRulers toggled.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }

    // Returns true if the extension corresponds to a supported Visio format
    private static bool IsVisioExtension(string ext)
    {
        return ext switch
        {
            ".vsdx" => true,
            ".vsd"  => true,
            ".vdx"  => true,
            ".vsx"  => true,
            ".vtx"  => true,
            ".vssx" => true,
            ".vstx" => true,
            ".vsdm" => true,
            ".vstm" => true,
            ".vssm" => true,
            ".vst"  => true,
            ".vss"  => true,
            _       => false,
        };
    }

    // Maps a file extension to the corresponding SaveFileFormat enum value
    private static SaveFileFormat GetSaveFileFormat(string ext)
    {
        return ext switch
        {
            ".vsdx" => SaveFileFormat.Vsdx,
            ".vsd"  => SaveFileFormat.Vsd,
            ".vdx"  => SaveFileFormat.Vdx,
            ".vsx"  => SaveFileFormat.Vsx,
            ".vtx"  => SaveFileFormat.Vtx,
            ".vssx" => SaveFileFormat.Vssx,
            ".vstx" => SaveFileFormat.Vstx,
            ".vsdm" => SaveFileFormat.Vsdm,
            ".vstm" => SaveFileFormat.Vstm,
            ".vssm" => SaveFileFormat.Vssm,
            ".vst"  => SaveFileFormat.Vst,
            ".vss"  => SaveFileFormat.Vss,
            _       => SaveFileFormat.Vsdx, // fallback
        };
    }
}