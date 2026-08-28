using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Get the folder path from command line or ask the user
        string folderPath = args.Length > 0 ? args[0] : PromptFolderPath();

        // Verify the folder exists before proceeding
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Define supported Visio file extensions
        string[] supportedExtensions = new[] { ".vsdx", ".vsd", ".vsdm", ".vssx", ".vstx", ".vssm", ".vstm", ".vsx", ".vtx" };
        var files = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        foreach (var file in files)
        {
            // Guard: ensure the file actually exists before processing
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"File not found: {file}");
                continue;
            }

            string ext = Path.GetExtension(file);
            // Skip files with unsupported extensions
            if (Array.IndexOf(supportedExtensions, ext.ToLower()) < 0)
                continue;

            try
            {
                // Load the diagram from the file
                Diagram diagram = new Diagram(file);

                // Access the global header/footer settings
                var headerFooter = diagram.HeaderFooter;
                var font = headerFooter.HeaderFooterFont;

                // Apply uniform font settings: Arial, Bold (weight 700), 12pt size (Height = -16), not italic, not underlined
                font.FaceName = "Arial";
                font.Weight = 700;          // 700 = Bold, 400 = Regular
                font.Height = -16;          // -16 corresponds to 12pt (approx)
                font.Italic = BOOL.False;
                font.Underline = BOOL.False;

                // Determine the appropriate save format based on the file extension
                SaveFileFormat format = GetSaveFormat(ext);
                // Save the diagram back to the original file using the determined format
                diagram.Save(file, format);

                Console.WriteLine($"Processed: {Path.GetFileName(file)}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing of the current file
                Console.Error.WriteLine($"Error processing file '{file}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }

    // Prompt the user for a folder path if not supplied via arguments
    private static string PromptFolderPath()
    {
        Console.Write("Enter the full path of the folder containing Visio diagrams: ");
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }

    // Map file extensions to the appropriate SaveFileFormat enum value
    private static SaveFileFormat GetSaveFormat(string extension)
    {
        switch (extension.ToLower())
        {
            case ".vsdx": return SaveFileFormat.Vsdx;
            case ".vsd":  return SaveFileFormat.Vsd;
            case ".vsdm": return SaveFileFormat.Vsdm;
            case ".vsx":  return SaveFileFormat.Vsx;
            case ".vtx":  return SaveFileFormat.Vtx;
            case ".vssx": return SaveFileFormat.Vssx;
            case ".vssm": return SaveFileFormat.Vssm;
            case ".vstx": return SaveFileFormat.Vstx;
            case ".vstm": return SaveFileFormat.Vstm;
            default:      return SaveFileFormat.Vsdx; // fallback
        }
    }
}