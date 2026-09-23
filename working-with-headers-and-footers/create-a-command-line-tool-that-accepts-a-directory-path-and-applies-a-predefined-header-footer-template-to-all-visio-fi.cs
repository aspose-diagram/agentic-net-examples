using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
    {
        static void Main(string[] args)
        {
            // Verify that a directory path was provided
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: VisioHeaderFooterTool <directoryPath>");
                return;
            }

            string directoryPath = args[0];

            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine($"Error: Directory does not exist - {directoryPath}");
                return;
            }

            // Process supported Visio file extensions
            string[] supportedExtensions = new[] { ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx" };
            string[] files = Directory.GetFiles(directoryPath);

            foreach (string filePath in files)
            {
                string ext = Path.GetExtension(filePath).ToLowerInvariant();
                if (Array.IndexOf(supportedExtensions, ext) < 0)
                {
                    // Skip non‑Visio files
                    continue;
                }

                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(filePath);

                    // Apply header/footer template
                    diagram.HeaderFooter.HeaderLeft = "Company Name";
                    diagram.HeaderFooter.HeaderCenter = "Document Title";
                    diagram.HeaderFooter.HeaderRight = "Confidential";

                    diagram.HeaderFooter.FooterLeft = "Created: &d";
                    diagram.HeaderFooter.FooterCenter = "";
                    diagram.HeaderFooter.FooterRight = "Page: &p of &P";

                    // Set margins (in inches)
                    diagram.HeaderFooter.HeaderMargin.Value = 0.3;
                    diagram.HeaderFooter.FooterMargin.Value = 0.3;

                    // Configure global header/footer font
                    var headerFooterFont = diagram.HeaderFooter.HeaderFooterFont;
                    headerFooterFont.FaceName = "Arial";
                    headerFooterFont.Weight = 700;          // Bold
                    headerFooterFont.Height = -16;          // Approx. 12pt (12 * -1.333 = -16)
                    headerFooterFont.Italic = BOOL.False;
                    headerFooterFont.Underline = BOOL.False;

                    // Set font color
                    diagram.HeaderFooter.HeaderFooterColor = Color.Black;

                    // Determine appropriate SaveFileFormat based on original extension
                    SaveFileFormat format = GetSaveFileFormat(ext);

                    // Save the diagram back to the same file
                    diagram.Save(filePath, format);

                    Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to process {Path.GetFileName(filePath)}: {ex.Message}");
                }
            }
        }

        // Maps file extension to the corresponding SaveFileFormat enum value
        private static SaveFileFormat GetSaveFileFormat(string extension)
        {
            return extension switch
            {
                ".vsdx" => SaveFileFormat.Vsdx,
                ".vsd"  => SaveFileFormat.Vsd,
                ".vdx"  => SaveFileFormat.Vdx,
                ".vsx"  => SaveFileFormat.Vsx,
                ".vtx"  => SaveFileFormat.Vtx,
                _ => SaveFileFormat.Vsdx // Fallback (should not occur)
            };
        }
    }