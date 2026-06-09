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
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Preserve the current font name and size of the header/footer font
                var headerFont = diagram.HeaderFooter.HeaderFooterFont;
                string existingFaceName = headerFont.FaceName;
                int existingHeight = headerFont.Height; // Height is stored as a negative value per Aspose.Diagram spec

                // Apply bold (weight 700) and underline while keeping the original font name and size
                headerFont.Weight = 700;          // Bold weight
                headerFont.Underline = BOOL.True; // Underline
                headerFont.FaceName = existingFaceName;
                headerFont.Height = existingHeight;

                // (Optional) Set the center header text if needed
                // diagram.HeaderFooter.HeaderCenter = "Center Header Text";

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
