using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder to process. Use first argument if provided, otherwise current directory.
            string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder does not exist: {folderPath}");
                return;
            }

            // Process all Visio files with .vsdx or .vdx extensions.
            string[] visioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                string extension = Path.GetExtension(filePath).ToLowerInvariant();

                if (extension != ".vsdx" && extension != ".vdx")
                {
                    // Skip non‑Visio files.
                    continue;
                }

                try
                {
                    // Load the diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Access the global HeaderFooterFont object.
                    HeaderFooterFont font = diagram.HeaderFooter.HeaderFooterFont;

                    // Set font properties based on file type.
                    if (extension == ".vsdx")
                    {
                        // Example: Calibri, Bold (Weight 700), 12 pt.
                        font.FaceName = "Calibri";
                        font.Weight = 700; // Bold
                        // Height uses negative mapping: DesiredPt * -1.333, rounded.
                        // 12 pt → -16
                        font.Height = -16;
                        font.Italic = BOOL.False;
                        font.Underline = BOOL.False;
                    }
                    else // .vdx
                    {
                        // Example: Times New Roman, Regular (Weight 400), 15 pt.
                        font.FaceName = "Times New Roman";
                        font.Weight = 400; // Regular
                        // 15 pt → -20 (15 * -1.333 ≈ -20)
                        font.Height = -20;
                        font.Italic = BOOL.False;
                        font.Underline = BOOL.False;
                    }

                    // Optionally set footer text (e.g., page number) if not already set.
                    diagram.HeaderFooter.FooterRight = "Page: &p";

                    // Save the diagram back, preserving its original format.
                    if (extension == ".vsdx")
                    {
                        diagram.Save(filePath, SaveFileFormat.Vsdx);
                    }
                    else // .vdx
                    {
                        diagram.Save(filePath, SaveFileFormat.Vdx);
                    }

                    Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {Path.GetFileName(filePath)}: {ex.Message}");
                }
            }
        }
    }