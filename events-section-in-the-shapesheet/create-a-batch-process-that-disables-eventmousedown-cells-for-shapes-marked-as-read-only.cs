using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect input folder path, optional output folder path.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> [outputFolder]");
            return;
        }

        string inputFolder = args[0];
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        string outputFolder = args.Length > 1 ? args[1] : Path.Combine(inputFolder, "Modified");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Define supported Visio file extensions (lowercase for case‑insensitive comparison).
        var visioExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx",
            ".vssx", ".vstx", ".vss", ".vst", ".vsdm",
            ".vssm", ".vstm", ".vdw"
        };

        // Process each file in the input folder (non‑recursive).
        foreach (string filePath in Directory.GetFiles(inputFolder))
        {
            // Guard: ensure the file exists (redundant after GetFiles, but required by rules).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Skip files that do not have a supported Visio extension.
            string ext = Path.GetExtension(filePath);
            if (!visioExtensions.Contains(ext))
            {
                continue;
            }

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        bool isReadOnly = false;

                        // Examine user‑defined cells for a "ReadOnly" flag.
                        foreach (User userCell in shape.Users)
                        {
                            if (userCell.NameU != null &&
                                userCell.NameU.Equals("ReadOnly", StringComparison.OrdinalIgnoreCase) &&
                                userCell.Value != null &&
                                userCell.Value.Val != null &&
                                userCell.Value.Val.Equals("TRUE", StringComparison.OrdinalIgnoreCase))
                            {
                                isReadOnly = true;
                                break;
                            }
                        }

                        if (isReadOnly)
                        {
                            // Disable the closest available event cells (EventMouseDown is not exposed).
                            // Clear EventDrop formula.
                            shape.Event.EventDrop.Ufe.F = string.Empty;
                            // Clear EventXFMod formula as an additional safeguard.
                            shape.Event.EventXFMod.Ufe.F = string.Empty;
                        }
                    }
                }

                // Prepare the output file path (same file name in the output folder).
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));

                // Save the modified diagram in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Processed and saved: {outputPath}");
            }
            catch (Exception ex)
            {
                // Write any errors to the error stream.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}