using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve document title and version.
                string title = diagram.DocumentProps.Title ?? string.Empty;
                string version = diagram.Version ?? string.Empty;

                // Compose the footer watermark text.
                string footerText = $"{title} - {version}";

                // Set the footer text (centered) for the whole document.
                diagram.HeaderFooter.FooterCenter = footerText;

                // Optional: configure footer font appearance.
                HeaderFooterFont footerFont = diagram.HeaderFooter.HeaderFooterFont;
                footerFont.FaceName = "Arial";          // Font family.
                footerFont.Height = 12;                 // Font size in points.
                footerFont.Weight = 700;                // Bold weight.
                footerFont.Underline = BOOL.False;      // No underline.

                // Optional: set footer text color (black).
                diagram.HeaderFooter.HeaderFooterColor = Color.FromArgb(0, 0, 0);

                // Save the modified diagram. Using VSDX format for broad compatibility.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Footer watermark added successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}