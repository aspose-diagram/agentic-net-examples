using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input folder argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the folder containing Visio files as the first argument.");
            return;
        }

        string folderPath = args[0];

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Supported Visio extensions
        string[] supportedExtensions = new[] { ".vsdx", ".vsd", ".vdx", ".vssx", ".vstx", ".vss", ".vst" };

        // Iterate over each file in the folder
        foreach (string filePath in Directory.GetFiles(folderPath))
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();

            if (Array.IndexOf(supportedExtensions, extension) < 0)
            {
                // Skip unsupported files
                continue;
            }

            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(filePath);

                // Ensure there is at least one window; create one if none exist
                if (diagram.Windows.Count == 0)
                {
                    Window newWindow = new Window();
                    // Initialize with default ShowRulers value (set to True)
                    newWindow.ShowRulers = BOOL.True;
                    diagram.Windows.Add(newWindow);
                }

                // Toggle ShowRulers on the first window
                Window window = diagram.Windows[0];
                window.ShowRulers = (window.ShowRulers == BOOL.True) ? BOOL.False : BOOL.True;

                // Determine the appropriate SaveFileFormat based on file extension
                SaveFileFormat format = GetSaveFileFormat(extension);
                if (format == 0)
                {
                    Console.Error.WriteLine($"Unsupported file format for saving: {filePath}");
                    continue;
                }

                // Save the diagram, overwriting the original file
                diagram.Save(filePath, format);
                Console.WriteLine($"Toggled ShowRulers for: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Maps file extensions to the corresponding SaveFileFormat enum value.
    /// Returns 0 if the extension is not recognized.
    /// </summary>
    private static SaveFileFormat GetSaveFileFormat(string extension)
    {
        switch (extension)
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
                return 0; // Unknown format
        }
    }
}