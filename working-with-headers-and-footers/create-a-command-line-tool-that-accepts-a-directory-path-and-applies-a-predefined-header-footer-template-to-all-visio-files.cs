using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Validate command-line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: HeaderFooterTool <directoryPath>");
            return;
        }

        string directoryPath = args[0];

        // Verify the directory exists.
        if (!Directory.Exists(directoryPath))
        {
            Console.Error.WriteLine($"Directory does not exist: {directoryPath}");
            return;
        }

        // Define Visio file extensions to process.
        string[] visioExtensions = new[]
        {
            ".vsdx", ".vsd", ".vsx", ".vtx",
            ".vssx", ".vss", ".vstx", ".vst",
            ".vstm", ".vssm", ".vsdm"
        };

        // Retrieve all matching files in the directory.
        string[] files = Directory.GetFiles(directoryPath)
            .Where(f => visioExtensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase))
            .ToArray();

        foreach (string filePath in files)
        {
            // Guard against missing files (should not happen after GetFiles, but added per rules).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram inside a using block as required.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // ----- Apply header/footer template -----
                    diagram.HeaderFooter.HeaderLeft = "Company Confidential";
                    diagram.HeaderFooter.HeaderCenter = "Document Title";
                    diagram.HeaderFooter.HeaderRight = "Date: &d";

                    diagram.HeaderFooter.FooterLeft = "Created by: User";
                    diagram.HeaderFooter.FooterCenter = "";
                    diagram.HeaderFooter.FooterRight = "Page &p of &P";

                    // Set margins (in inches).
                    diagram.HeaderFooter.HeaderMargin.Value = 0.5;
                    diagram.HeaderFooter.FooterMargin.Value = 0.5;

                    // Configure header/footer font.
                    diagram.HeaderFooter.HeaderFooterFont.FaceName = "Arial";
                    diagram.HeaderFooter.HeaderFooterFont.Height = 12;
                    diagram.HeaderFooter.HeaderFooterFont.Weight = 700; // Bold weight.
                    diagram.HeaderFooter.HeaderFooterFont.Italic = BOOL.False;
                    diagram.HeaderFooter.HeaderFooterFont.Underline = BOOL.False;

                    // Set header/footer color (black).
                    diagram.HeaderFooter.HeaderFooterColor = Color.FromArgb(0, 0, 0);

                    // Determine the appropriate save format based on file extension.
                    SaveFileFormat format = GetSaveFormat(filePath);

                    // Save the modified diagram back to the original file.
                    diagram.Save(filePath, format);
                }

                Console.WriteLine($"Processed: {filePath}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }

    // Maps a file extension to the corresponding Aspose.Diagram SaveFileFormat.
    private static SaveFileFormat GetSaveFormat(string filePath)
    {
        string ext = Path.GetExtension(filePath).ToLowerInvariant();
        return ext switch
        {
            ".vsdx" => SaveFileFormat.Vsdx,
            ".vsd" => SaveFileFormat.Vsd,
            ".vsx" => SaveFileFormat.Vsx,
            ".vtx" => SaveFileFormat.Vtx,
            ".vssx" => SaveFileFormat.Vssx,
            ".vss" => SaveFileFormat.Vss,
            ".vstx" => SaveFileFormat.Vstx,
            ".vst" => SaveFileFormat.Vst,
            ".vstm" => SaveFileFormat.Vstm,
            ".vssm" => SaveFileFormat.Vssm,
            ".vsdm" => SaveFileFormat.Vsdm,
            _ => SaveFileFormat.Vsdx,
        };
    }
}