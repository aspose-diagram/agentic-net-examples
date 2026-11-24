using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the directory path containing Visio files as the first argument.");
            return;
        }

        string folderPath = args[0];

        // Verify the directory exists
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Directory does not exist: {folderPath}");
            return;
        }

        // Get all Visio files in the directory (common extensions)
        string[] visioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in visioFiles)
        {
            // Process only known Visio extensions
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".vsdx" && ext != ".vsd" && ext != ".vdx" && ext != ".vssx" && ext != ".vss" && ext != ".vstx" && ext != ".vst")
            {
                continue;
            }

            // Guard against missing file (should not happen after GetFiles, but added per rules)
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Update footer text with the current timestamp
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                diagram.HeaderFooter.FooterRight = $"Generated: {timestamp}";

                // Optionally set left/footer margin (example value)
                diagram.HeaderFooter.FooterMargin.Value = 0.2; // inches from page edge

                // Configure footer font
                var footerFont = diagram.HeaderFooter.HeaderFooterFont;
                footerFont.FaceName = "Arial";
                footerFont.Weight = 700;               // Bold
                footerFont.Height = 12;                // Font size (points)
                footerFont.Italic = BOOL.False;
                footerFont.Underline = BOOL.False;

                // Set footer text color
                diagram.HeaderFooter.HeaderFooterColor = Color.Black;

                // Save the diagram back in VSDX format (overwrites original)
                diagram.Save(filePath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Processed and saved: {filePath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}