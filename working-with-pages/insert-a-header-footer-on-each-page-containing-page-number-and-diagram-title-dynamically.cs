using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // -----------------------------------------------------------------
                // Configure Header: place the diagram title at the center.
                // -----------------------------------------------------------------
                string title = diagram.DocumentProps.Title;
                diagram.HeaderFooter.HeaderCenter = title ?? string.Empty;

                // -----------------------------------------------------------------
                // Configure Footer: show page number and total pages on the right.
                // Visio field codes: &p = current page number, &P = total pages.
                // -----------------------------------------------------------------
                diagram.HeaderFooter.FooterRight = "Page &p of &P";

                // -----------------------------------------------------------------
                // Optional: adjust margins (in inches) for better layout.
                // -----------------------------------------------------------------
                diagram.HeaderFooter.HeaderMargin.Value = 0.25; // top margin
                diagram.HeaderFooter.FooterMargin.Value = 0.25; // bottom margin

                // -----------------------------------------------------------------
                // Configure the font used for both header and footer.
                // -----------------------------------------------------------------
                HeaderFooterFont font = diagram.HeaderFooter.HeaderFooterFont;
                font.FaceName = "Arial";
                font.Height = 12;               // Font size (points)
                font.Weight = 700;              // 700 = Bold, 400 = Regular
                font.Italic = BOOL.False;
                font.Underline = BOOL.False;

                // -----------------------------------------------------------------
                // Save the modified diagram to the specified output path.
                // -----------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}