using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder containing Visio files.
            // If a folder path is provided as a command‑line argument, use it; otherwise use the current directory.
            string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder not found: {folderPath}");
                return;
            }

            // Supported Visio extensions.
            string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx", ".vssx", ".vstx", ".vsdm", ".vssm", ".vstm" };

            // Get all Visio files in the folder.
            var visioFiles = Directory.GetFiles(folderPath)
                                      .Where(f => extensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase))
                                      .ToList();

            if (!visioFiles.Any())
            {
                Console.WriteLine("No Visio files found in the specified folder.");
                return;
            }

            foreach (var filePath in visioFiles)
            {
                try
                {
                    // Load the diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Retrieve the document title (built‑in property).
                    string title = diagram.DocumentProps.Title ?? string.Empty;

                    // Set the left header text to the document title.
                    diagram.HeaderFooter.HeaderLeft = title;

                    // Save the diagram back to the same file in VSDX format.
                    // This overwrites the original file; change the path if you need a separate output folder.
                    diagram.Save(filePath, SaveFileFormat.Vsdx);

                    Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Header update completed.");
        }
    }