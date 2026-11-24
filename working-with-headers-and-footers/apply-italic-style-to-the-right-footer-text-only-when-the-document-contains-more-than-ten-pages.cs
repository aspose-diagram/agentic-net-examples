using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Check if the document contains more than ten pages.
            if (diagram.Pages.Count > 10)
            {
                // Retrieve the current right‑footer text (if any).
                string currentFooterRight = diagram.HeaderFooter.FooterRight;

                // NOTE: Aspose.Diagram applies font styling globally to all header/footer
                // text via HeaderFooterFont. Per‑section styling (e.g., only right footer)
                // is not supported, so we apply Italic to the global font as the closest
                // achievable alternative.

                // Apply italic style.
                diagram.HeaderFooter.HeaderFooterFont.Italic = BOOL.True;

                // Preserve the existing right‑footer text.
                diagram.HeaderFooter.FooterRight = currentFooterRight;
            }

            // Prepare output path.
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_modified.vsdx");

            // Save the modified diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram processed successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}