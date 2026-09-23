using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram (blank Visio document)
        Diagram diagram = new Diagram();

        // Ensure the document has a title; set a default if missing
        if (string.IsNullOrWhiteSpace(diagram.DocumentProps.Title))
        {
            diagram.DocumentProps.Title = "My Document Title";
        }

        // Configure the header font: Arial, 12 point (Height = -16 per Aspose.Diagram scaling)
        var headerFont = diagram.HeaderFooter.HeaderFooterFont;
        headerFont.FaceName = "Arial";
        headerFont.Height = -16; // 12 pt font
        headerFont.Weight = 400; // regular weight (not bold)

        // Set the header text (centered) to the document title
        diagram.HeaderFooter.HeaderCenter = diagram.DocumentProps.Title;

        // Save the diagram to a VSDX file
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
