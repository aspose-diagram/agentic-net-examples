using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder to process
            string folderPath;
            if (args.Length > 0)
            {
                folderPath = args[0];
            }
            else
            {
                Console.Write("Enter the full path of the folder containing Visio files: ");
                folderPath = Console.ReadLine();
            }

            if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
            {
                Console.WriteLine("Invalid folder path.");
                return;
            }

            // Supported Visio extensions
            string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx", ".vssx", ".vstx", ".vsdm", ".vssm", ".vstm" };

            // Process each file
            foreach (string filePath in Directory.GetFiles(folderPath))
            {
                string ext = Path.GetExtension(filePath).ToLowerInvariant();
                if (Array.IndexOf(extensions, ext) < 0)
                    continue; // Skip non‑Visio files

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Apply uniform header/footer font settings
                    HeaderFooter headerFooter = diagram.HeaderFooter;
                    HeaderFooterFont hfFont = headerFooter.HeaderFooterFont;

                    // Font family
                    hfFont.FaceName = "Arial";

                    // Bold weight (700 = Bold, 400 = Regular)
                    hfFont.Weight = 700;

                    // Point size 12pt -> Height = -16 (12 * -1.333 ≈ -16)
                    hfFont.Height = -16;

                    // Italic and underline flags
                    hfFont.Italic = BOOL.False;
                    hfFont.Underline = BOOL.False;

                    // Text color (black)
                    headerFooter.HeaderFooterColor = Color.Black;

                    // Save back in the same format (using Vsdx as a safe default)
                    diagram.Save(filePath, SaveFileFormat.Vsdx);

                    Console.WriteLine($"Updated header/footer fonts for: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }