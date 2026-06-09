using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder containing the diagrams.
            // If a folder path is provided as the first argument, use it; otherwise use the current directory.
            string inputFolder = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            // Get all Visio files in the folder (common extensions).
            string[] diagramFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in diagramFiles)
            {
                // Process only files with known Visio extensions.
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (extension != ".vsdx" && extension != ".vsd" && extension != ".vssx" && extension != ".vstx")
                {
                    continue;
                }

                try
                {
                    // Load the diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Apply a uniform header margin of 0.25 inches.
                    // HeaderFooter.HeaderMargin is a DoubleValue; assign the inches directly.
                    diagram.HeaderFooter.HeaderMargin.Value = 0.25;

                    // Save the diagram back to the same file, preserving its original format.
                    // Use the appropriate SaveFileFormat based on the file extension.
                    SaveFileFormat format = extension switch
                    {
                        ".vsdx" => SaveFileFormat.Vsdx,
                        ".vsd" => SaveFileFormat.Vsd,
                        ".vssx" => SaveFileFormat.Vssx,
                        ".vstx" => SaveFileFormat.Vstx,
                        _ => SaveFileFormat.Vsdx
                    };

                    diagram.Save(filePath, format);
                    Console.WriteLine($"Successfully updated header margin for: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    // Log any failures without stopping the batch process.
                    Console.WriteLine($"Failed to process {Path.GetFileName(filePath)}: {ex.Message}");
                }
            }
        }
    }