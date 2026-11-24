using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Create a backup file path by inserting "_backup" before the extension
        string backupPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_backup" + Path.GetExtension(inputPath));

        try
        {
            // Load the original diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Save a backup copy of the original diagram (same format as input)
                diagram.Save(backupPath, SaveFileFormat.Vsdx);

                // ------------------------------------------------------------
                // Apply watermark changes: add a full‑page text shape as a watermark
                // ------------------------------------------------------------

                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages to add a watermark.");
                    return;
                }

                // Use the first page for the watermark
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Add a text shape that covers the whole page.
                // Parameters: pinX, pinY, width, height, text, fontName, fontColor, fontSize (in inches)
                page.AddText(
                    0.0,
                    0.0,
                    pageWidth,
                    pageHeight,
                    "Watermark",
                    "Calibri",
                    "#A5A5A5",
                    0.25); // 0.25 inches ≈ 18 points

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
