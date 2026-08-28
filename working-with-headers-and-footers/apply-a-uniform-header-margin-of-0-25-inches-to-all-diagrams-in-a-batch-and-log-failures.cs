using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Folder containing the Visio files to process.
            string inputFolder = @"C:\VisioFiles";
            // Optional: folder to save the updated files.
            string outputFolder = @"C:\VisioFiles\Processed";

            // Ensure the output folder exists.
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // Process each Visio file in the input folder.
            string[] files = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in files)
            {
                // Consider only supported Visio extensions.
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx" &&
                    extension != ".vsx" && extension != ".vtx" && extension != ".vsdm")
                {
                    continue;
                }

                try
                {
                    // Load the diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Apply a uniform header margin of 0.25 inches.
                    diagram.HeaderFooter.HeaderMargin.Value = 0.25;

                    // Determine the save format based on the original extension.
                    SaveFileFormat format = extension switch
                    {
                        ".vsdx" => SaveFileFormat.Vsdx,
                        ".vsd"  => SaveFileFormat.Vsd,
                        ".vdx"  => SaveFileFormat.Vdx,
                        ".vsx"  => SaveFileFormat.Vsx,
                        ".vtx"  => SaveFileFormat.Vtx,
                        ".vsdm" => SaveFileFormat.Vsdm,
                        _       => SaveFileFormat.Vsdx
                    };

                    // Build the output file path.
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));

                    // Save the updated diagram.
                    diagram.Save(outputPath, format);

                    Console.WriteLine($"Successfully processed: {filePath}");
                }
                catch (Exception ex)
                {
                    // Log any failures without stopping the batch.
                    Console.WriteLine($"Failed to process: {filePath}");
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }