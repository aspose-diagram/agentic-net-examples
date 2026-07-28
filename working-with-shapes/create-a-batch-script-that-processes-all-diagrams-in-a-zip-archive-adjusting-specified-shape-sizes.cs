using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: path to the zip archive and output directory.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramBatchProcessor <zipPath> <outputDirectory>");
                return;
            }

            string zipPath = args[0];
            string outputDir = args[1];

            if (!File.Exists(zipPath))
            {
                Console.WriteLine($"Zip file not found: {zipPath}");
                return;
            }

            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Open the zip archive for reading.
            using (FileStream zipFileStream = new FileStream(zipPath, FileMode.Open, FileAccess.Read))
            using (ZipArchive archive = new ZipArchive(zipFileStream, ZipArchiveMode.Read))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    // Process only Visio diagram files based on extension.
                    string extension = Path.GetExtension(entry.FullName);
                    if (!IsVisioFile(extension))
                    {
                        continue;
                    }

                    Console.WriteLine($"Processing: {entry.FullName}");

                    // Load the diagram from the entry stream.
                    using (Stream entryStream = entry.Open())
                    using (MemoryStream diagramStream = new MemoryStream())
                    {
                        entryStream.CopyTo(diagramStream);
                        diagramStream.Position = 0;

                        Diagram diagram = new Diagram(diagramStream);

                        // Adjust shape sizes as required.
                        AdjustShapeSizes(diagram);

                        // Prepare output file path.
                        string outputPath = Path.Combine(outputDir, Path.GetFileName(entry.FullName));

                        // Save the modified diagram in the same format as the original.
                        SaveFileFormat format = GetSaveFormatFromExtension(extension);
                        diagram.Save(outputPath, format);
                    }
                }
            }

            Console.WriteLine("Batch processing completed.");
        }

        // Determines whether the file extension corresponds to a supported Visio format.
        private static bool IsVisioFile(string extension)
        {
            if (string.IsNullOrEmpty(extension))
                return false;

            string ext = extension.ToLowerInvariant();
            return ext == ".vsdx" || ext == ".vsd" || ext == ".vdx" || ext == ".vssx" ||
                   ext == ".vss" || ext == ".vstx" || ext == ".vst" || ext == ".vtx";
        }

        // Maps file extension to the appropriate SaveFileFormat enum value.
        private static SaveFileFormat GetSaveFormatFromExtension(string extension)
        {
            string ext = extension.ToLowerInvariant();
            return ext switch
            {
                ".vsdx" => SaveFileFormat.Vsdx,
                ".vsd" => SaveFileFormat.Vsd,
                ".vdx" => SaveFileFormat.Vdx,
                ".vssx" => SaveFileFormat.Vssx,
                ".vss" => SaveFileFormat.Vss,
                ".vstx" => SaveFileFormat.Vstx,
                ".vst" => SaveFileFormat.Vst,
                ".vtx" => SaveFileFormat.Vtx,
                _ => SaveFileFormat.Vsdx // Default fallback.
            };
        }

        // Adjusts the size of specific shapes within the diagram.
        private static void AdjustShapeSizes(Diagram diagram)
        {
            // Example criteria: adjust all shapes whose universal name is "Rectangle".
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check shape name (case-insensitive).
                    if (string.Equals(shape.NameU, "Rectangle", StringComparison.OrdinalIgnoreCase))
                    {
                        // Set new width and height (in inches).
                        shape.XForm.Width.Value = 2.0;   // 2 inches wide.
                        shape.XForm.Height.Value = 1.0;  // 1 inch tall.

                        Console.WriteLine($"Adjusted shape ID {shape.ID} on page '{page.Name}' to 2\" x 1\".");
                    }
                }
            }
        }
    }