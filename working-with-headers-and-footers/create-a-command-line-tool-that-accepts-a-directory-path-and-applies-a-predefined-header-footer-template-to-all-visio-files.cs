using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length != 1)
        {
            Console.Error.WriteLine("Usage: HeaderFooterTool <directoryPath>");
            return;
        }

        string directoryPath = args[0];

        // Verify the directory exists.
        if (!Directory.Exists(directoryPath))
        {
            Console.Error.WriteLine($"Error: Directory does not exist – {directoryPath}");
            return;
        }

        // Define the header/footer template strings.
        const string leftHeader   = "Company Name";
        const string centerHeader = "Document Title";
        const string rightHeader  = "Date: &d";          // Visio date field.
        const string leftFooter   = "Page &p of &P";    // Visio page number fields.
        const string centerFooter = "";
        const string rightFooter  = "Confidential";

        // Map supported file extensions to their corresponding SaveFileFormat.
        var formatMap = new Dictionary<string, SaveFileFormat>(StringComparer.OrdinalIgnoreCase)
        {
            { ".vsdx", SaveFileFormat.Vsdx },
            { ".vsd",  SaveFileFormat.Vsd },
            { ".vsx",  SaveFileFormat.Vsx },
            { ".vtx",  SaveFileFormat.Vtx }
        };

        // Retrieve all files in the specified directory (non‑recursive).
        string[] allFiles = Directory.GetFiles(directoryPath);

        foreach (string filePath in allFiles)
        {
            // Guard: ensure the file still exists before processing.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"Skipping missing file: {filePath}");
                continue;
            }

            string extension = Path.GetExtension(filePath);
            if (!formatMap.TryGetValue(extension, out SaveFileFormat saveFormat))
            {
                // Not a supported Visio file; skip.
                continue;
            }

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(filePath);

                // Apply header strings.
                diagram.HeaderFooter.HeaderLeft   = leftHeader;
                diagram.HeaderFooter.HeaderCenter = centerHeader;
                diagram.HeaderFooter.HeaderRight  = rightHeader;

                // Apply footer strings.
                diagram.HeaderFooter.FooterLeft   = leftFooter;
                diagram.HeaderFooter.FooterCenter = centerFooter;
                diagram.HeaderFooter.FooterRight  = rightFooter;

                // Optional: configure a simple header/footer font.
                var hfFont = diagram.HeaderFooter.HeaderFooterFont;
                hfFont.FaceName = "Arial";
                hfFont.Weight   = 700;          // Bold.
                hfFont.Height   = -16;          // Approx. 12pt (negative value per API spec).
                hfFont.Underline = BOOL.False; // No underline.

                // Save the diagram, overwriting the original file.
                diagram.Save(filePath, saveFormat);

                Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}