using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (or stencil) that contains the masters.
                string diagramPath = "input.vsdx";

                // Folder where PNG thumbnails will be saved.
                string outputFolder = "MasterThumbnails";

                // Ensure the output directory exists.
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                // Load the Visio diagram.
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all masters in the diagram.
                foreach (Master master in diagram.Masters)
                {
                    // The master icon is stored as a byte array (PNG format).
                    byte[] iconData = master.Icon;

                    // Skip masters without an icon.
                    if (iconData == null || iconData.Length == 0)
                    {
                        continue;
                    }

                    // Build a safe file name for the thumbnail.
                    string safeName = string.IsNullOrWhiteSpace(master.Name) ? "UnnamedMaster" : master.Name;
                    // Replace any invalid filename characters.
                    foreach (char c in Path.GetInvalidFileNameChars())
                    {
                        safeName = safeName.Replace(c, '_');
                    }

                    string outputPath = Path.Combine(outputFolder, $"{safeName}.png");

                    // Write the PNG data to disk.
                    File.WriteAllBytes(outputPath, iconData);

                    Console.WriteLine($"Exported thumbnail for master '{master.Name}' to '{outputPath}'.");
                }

                Console.WriteLine("Thumbnail export completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }