using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the diagram title (built‑in document property)
            string title = diagram.DocumentProps.Title;

            // Set header and footer text.
            // Header center will display the diagram title.
            diagram.HeaderFooter.HeaderCenter = title;

            // Footer right will display the page number using Visio field code '&p'.
            diagram.HeaderFooter.FooterRight = "Page: &p";

            // Optional: customize the appearance of header/footer text.
            // Set font face, weight (700 = bold), and size (negative value maps to points).
            var headerFooterFont = diagram.HeaderFooter.HeaderFooterFont;
            headerFooterFont.FaceName = "Arial";
            headerFooterFont.Weight = 700;      // Bold
            headerFooterFont.Height = -16;      // Approx. 12 pt (16 × ‑1.333 ≈ 12 pt)

            // Set the text color to black.
            diagram.HeaderFooter.HeaderFooterColor = Color.Black;

            // Save the updated diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
