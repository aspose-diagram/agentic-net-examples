using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages (demonstration; header/footer is global)
            foreach (Page page in diagram.Pages)
            {
                Console.WriteLine($"Processing page: {page.Name}");
                // No per‑page header/footer; the HeaderFooter object is global.
            }

            // Replace the font used in the document header/footer with a localized font.
            // The HeaderFooterFont object provides direct access to font properties.
            var headerFont = diagram.HeaderFooter.HeaderFooterFont;
            headerFont.FaceName = "Arial Unicode MS";   // Localized font name
            headerFont.Weight = 700;                    // Bold (700 = Bold, 400 = Regular)
            headerFont.Height = -16;                    // Approx. 12 pt (negative value per spec)

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Diagram saved with updated header/footer font.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
