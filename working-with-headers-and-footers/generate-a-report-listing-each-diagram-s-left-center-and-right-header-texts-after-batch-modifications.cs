using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder containing the diagrams.
            // If a path is passed as the first argument, use it; otherwise, use the current directory.
            string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Error: The folder \"{folderPath}\" does not exist.");
                return;
            }

            // Get all Visio files in the folder (common extensions).
            string[] diagramFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
            string[] supportedExtensions = { ".vsdx", ".vsd", ".vdx", ".vssx", ".vss", ".vstx", ".vst", ".vtx" };

            foreach (string filePath in diagramFiles)
            {
                string extension = Path.GetExtension(filePath);
                if (Array.IndexOf(supportedExtensions, extension, 0, supportedExtensions.Length) < 0)
                {
                    // Skip non‑Visio files.
                    continue;
                }

                try
                {
                    // Load the diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Retrieve header texts. Use empty string if any are null.
                    string leftHeader   = diagram.HeaderFooter.HeaderLeft   ?? string.Empty;
                    string centerHeader = diagram.HeaderFooter.HeaderCenter ?? string.Empty;
                    string rightHeader  = diagram.HeaderFooter.HeaderRight  ?? string.Empty;

                    // Output the information.
                    Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"  Header Left:   \"{leftHeader}\"");
                    Console.WriteLine($"  Header Center: \"{centerHeader}\"");
                    Console.WriteLine($"  Header Right:  \"{rightHeader}\"");
                    Console.WriteLine(); // Blank line for readability
                }
                catch (Exception ex)
                {
                    // Report any errors loading or processing the diagram.
                    Console.WriteLine($"Failed to process \"{Path.GetFileName(filePath)}\": {ex.Message}");
                }
            }
        }
    }