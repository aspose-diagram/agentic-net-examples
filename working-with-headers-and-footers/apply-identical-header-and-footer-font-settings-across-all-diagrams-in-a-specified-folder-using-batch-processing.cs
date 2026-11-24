using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

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

        // Guard: ensure the folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Set a global fallback font (used when a required font is missing).
        FontConfigs.DefaultFontName = "Arial";

        // Define the desired header/footer font settings.
        const string desiredFaceName = "Arial";
        const int desiredWeight = 400;          // 400 = regular, 700 = bold
        const int desiredHeight = -16;          // Approx. 12pt (12 * -1.333 ≈ -16)
        const BOOL desiredItalic = BOOL.False;
        const BOOL desiredUnderline = BOOL.False;

        // Process each Visio file in the folder.
        string[] supportedExtensions = new[] { ".vsdx", ".vsd", ".vsx", ".vtx", ".vssx", ".vss", ".vstx", ".vst" };
        foreach (string filePath in Directory.GetFiles(folderPath))
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (Array.IndexOf(supportedExtensions, extension) < 0)
            {
                // Skip non‑Visio files.
                continue;
            }

            try
            {
                // Load the diagram inside a using block (mandatory pattern).
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Access the global HeaderFooterFont object.
                    HeaderFooterFont headerFooterFont = diagram.HeaderFooter.HeaderFooterFont;

                    // Apply the desired typography settings.
                    headerFooterFont.FaceName = desiredFaceName;
                    headerFooterFont.Weight = desiredWeight;
                    headerFooterFont.Height = desiredHeight;
                    headerFooterFont.Italic = desiredItalic;
                    headerFooterFont.Underline = desiredUnderline;

                    // Optionally set the header/footer text color.
                    diagram.HeaderFooter.HeaderFooterColor = Color.Black;

                    // Determine the appropriate SaveFileFormat based on the original extension.
                    SaveFileFormat format = GetSaveFormatFromExtension(extension);

                    // Save the modified diagram, overwriting the original file.
                    diagram.Save(filePath, format);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of a single file.
                Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }
    }

    // Helper method to map file extensions to SaveFileFormat enum values.
    private static SaveFileFormat GetSaveFormatFromExtension(string extension)
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
            default:      return SaveFileFormat.Vsdx; // Fallback (should not reach here).
        }
    }
}