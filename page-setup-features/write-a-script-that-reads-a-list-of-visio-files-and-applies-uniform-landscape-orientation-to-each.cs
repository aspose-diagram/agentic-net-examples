using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Define the folder containing Visio files.
            // You can change this path or pass it as a command‑line argument.
            string folderPath = args.Length > 0 ? args[0] : @"C:\VisioFiles";

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder not found: {folderPath}");
                return;
            }

            // Supported Visio extensions.
            string[] extensions = new[] { ".vsdx", ".vdx", ".vsd", ".vsx", ".vtx", ".vssx", ".vstx", ".vsdm", ".vssm", ".vstm", ".vss", ".vst", ".vsd" };

            // Get all Visio files in the folder (non‑recursive).
            string[] files = Directory.GetFiles(folderPath);
            foreach (string filePath in files)
            {
                string ext = Path.GetExtension(filePath).ToLowerInvariant();
                if (Array.IndexOf(extensions, ext) < 0)
                    continue; // Skip non‑Visio files.

                try
                {
                    // Load the diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Apply Landscape orientation to every page.
                    foreach (Page page in diagram.Pages)
                    {
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    }

                    // Determine the appropriate SaveFileFormat based on the file extension.
                    SaveFileFormat format = ext switch
                    {
                        ".vsdx" => SaveFileFormat.Vsdx,
                        ".vdx" => SaveFileFormat.Vdx,
                        ".vsd" => SaveFileFormat.Vsd,
                        ".vsx" => SaveFileFormat.Vsx,
                        ".vtx" => SaveFileFormat.Vtx,
                        ".vssx" => SaveFileFormat.Vssx,
                        ".vstx" => SaveFileFormat.Vstx,
                        ".vsdm" => SaveFileFormat.Vsdm,
                        ".vssm" => SaveFileFormat.Vssm,
                        ".vstm" => SaveFileFormat.Vstm,
                        ".vss" => SaveFileFormat.Vss,
                        ".vst" => SaveFileFormat.Vst,
                        _ => SaveFileFormat.Vsdx // Fallback to VSDX if unknown.
                    };

                    // Save the modified diagram, overwriting the original file.
                    diagram.Save(filePath, format);
                    Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing '{Path.GetFileName(filePath)}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch orientation update completed.");
        }
    }