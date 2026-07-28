using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Directory containing Visio files; adjust as needed.
        string folderPath = @"C:\VisioFiles";

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Process each .vsdx and .vdx file in the folder.
        string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in files)
        {
            string extension = Path.GetExtension(filePath);
            if (!extension.Equals(".vsdx", StringComparison.OrdinalIgnoreCase) &&
                !extension.Equals(".vdx", StringComparison.OrdinalIgnoreCase))
            {
                continue; // Skip unsupported files.
            }

            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(filePath);

                // Access the global HeaderFooter object.
                HeaderFooter headerFooter = diagram.HeaderFooter;

                // Set a common footer text with page number placeholder.
                headerFooter.FooterRight = "Page: &p";

                // Configure font based on file extension.
                HeaderFooterFont font = headerFooter.HeaderFooterFont;

                if (extension.Equals(".vsdx", StringComparison.OrdinalIgnoreCase))
                {
                    // VSDX files: use Calibri, bold, approx. 12pt.
                    font.FaceName = "Calibri";
                    font.Weight = 700;               // Bold.
                    font.Height = -16;               // Approx. 12pt (12 * -1.333 ≈ -16).
                }
                else // .vdx
                {
                    // VDX files: use Arial, regular weight, approx. 9pt.
                    font.FaceName = "Arial";
                    font.Weight = 400;               // Regular.
                    font.Height = -12;               // Approx. 9pt (9 * -1.333 ≈ -12).
                }

                // Optional: adjust footer margin (0.5 inches from bottom).
                headerFooter.FooterMargin.Value = 0.5;

                // Save the diagram back to the same file.
                diagram.Save(filePath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Processed and saved: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{Path.GetFileName(filePath)}': {ex.Message}");
            }
        }
    }
}
