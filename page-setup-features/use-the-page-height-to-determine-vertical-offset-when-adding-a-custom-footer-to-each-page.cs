using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputVisioFilePath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path (optional second argument or default suffix)
        string outputPath = args.Length >= 2
            ? args[1]
            : Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_FooterAdded.vsdx");

        // Load the Visio diagram inside a using block (mandatory pattern)
        using (Diagram diagram = new Diagram(inputPath))
        {
            try
            {
                // Desired distance of the footer from the top edge (in inches)
                double desiredTopOffset = 0.5;

                // Calculate the footer margin (distance from the bottom edge) based on the first page height
                double footerMargin = 0.0;
                if (diagram.Pages.Count > 0)
                {
                    Page firstPage = diagram.Pages[0];
                    double pageHeight = firstPage.PageSheet.PageProps.PageHeight.Value; // height in inches
                    // FooterMargin = page height - desired distance from top
                    footerMargin = pageHeight - desiredTopOffset;
                }

                // Set custom footer text (applies to the whole document)
                diagram.HeaderFooter.FooterLeft = "Custom Footer - Left";
                diagram.HeaderFooter.FooterCenter = "Custom Footer - Center";
                diagram.HeaderFooter.FooterRight = "Custom Footer - Right";

                // Apply the calculated vertical offset using FooterMargin (distance from bottom edge)
                diagram.HeaderFooter.FooterMargin.Value = footerMargin;

                // Customize footer font
                var footerFont = diagram.HeaderFooter.HeaderFooterFont;
                footerFont.FaceName = "Arial";
                // Negative Height maps to point size (e.g., -16 ≈ 12pt)
                footerFont.Height = -16;
                footerFont.Weight = 700; // 700 = bold
                footerFont.Underline = BOOL.False; // no underline

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with custom footer to: {outputPath}");
            }
            catch (Exception ex)
            {
                // Write any errors to the error console
                Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            }
        }
    }
}