using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Input folder containing Visio files
            string inputFolder = @"C:\VisioFiles";
            // Output folder for processed files
            string outputFolder = Path.Combine(inputFolder, "Processed");

            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // Get all Visio files in the folder (common extensions)
            string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (!IsVisioExtension(extension))
                    continue; // Skip non‑Visio files

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Apply Landscape orientation to every page
                    foreach (Page page in diagram.Pages)
                    {
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    }

                    // Determine the appropriate SaveFileFormat based on the original extension
                    SaveFileFormat format = GetSaveFileFormat(extension);

                    // Build output file path
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));

                    // Save the modified diagram
                    diagram.Save(outputPath, format);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }

        // Checks if the file extension corresponds to a supported Visio format
        private static bool IsVisioExtension(string ext)
        {
            return ext == ".vsdx" || ext == ".vsd" || ext == ".vdx" ||
                   ext == ".vsx" || ext == ".vtx" || ext == ".vssx" ||
                   ext == ".vstx" || ext == ".vsdm" || ext == ".vssm" ||
                   ext == ".vstm" || ext == ".vss" || ext == ".vst";
        }

        // Maps file extensions to the corresponding SaveFileFormat enum values
        private static SaveFileFormat GetSaveFileFormat(string ext)
        {
            return ext switch
            {
                ".vsdx" => SaveFileFormat.Vsdx,
                ".vsd"  => SaveFileFormat.Vsd,
                ".vdx"  => SaveFileFormat.Vdx,
                ".vsx"  => SaveFileFormat.Vsx,
                ".vtx"  => SaveFileFormat.Vtx,
                ".vssx" => SaveFileFormat.Vssx,
                ".vstx" => SaveFileFormat.Vstx,
                ".vsdm" => SaveFileFormat.Vsdm,
                ".vssm" => SaveFileFormat.Vssm,
                ".vstm" => SaveFileFormat.Vstm,
                ".vss"  => SaveFileFormat.Vss,
                ".vst"  => SaveFileFormat.Vst,
                _ => throw new NotSupportedException($"Unsupported Visio file extension: {ext}")
            };
        }
    }