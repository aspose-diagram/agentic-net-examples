using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Insert automatic page numbering aligned to the right margin
            diagram.HeaderFooter.FooterRight = "Page: &p";

            // Optional: set the distance of the footer from the bottom edge (in inches)
            diagram.HeaderFooter.FooterMargin.Value = 0.5;

            // Optional: configure footer typography (font, weight, size)
            var font = diagram.HeaderFooter.HeaderFooterFont;
            font.FaceName = "Calibri";
            font.Weight = 700;          // Bold
            font.Height = -16;          // Approx. 12pt (desired point size * -1.333)

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
