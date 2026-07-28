using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Folder containing Visio files (adjust as needed)
            string inputFolder = @"C:\VisioFiles";

            // Get all Visio files in the folder (common extensions)
            string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx" && extension != ".vsx" && extension != ".vtx")
                {
                    // Skip non‑Visio files
                    continue;
                }

                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Ensure the document has at least one window before accessing it
                if (diagram.Windows.Count > 0)
                {
                    // Set ShowGuides to false for the first window
                    diagram.Windows[0].ShowGuides = BOOL.False;
                }

                // Save the diagram back to the same file (preserving original format)
                // Choose the appropriate SaveFileFormat based on the file extension
                SaveFileFormat format = extension switch
                {
                    ".vsdx" => SaveFileFormat.Vsdx,
                    ".vsd"  => SaveFileFormat.Vsd,
                    ".vdx"  => SaveFileFormat.Vdx,
                    ".vsx"  => SaveFileFormat.Vsx,
                    ".vtx"  => SaveFileFormat.Vtx,
                    _       => SaveFileFormat.Vsdx // fallback
                };

                diagram.Save(filePath, format);
                Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
            }

            Console.WriteLine("Batch processing completed.");
        }
    }