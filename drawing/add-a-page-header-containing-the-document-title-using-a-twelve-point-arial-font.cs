using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new diagram (or load an existing one)
            Diagram diagram = new Diagram();

            // Ensure the document has a title; this will be used in the header
            if (string.IsNullOrWhiteSpace(diagram.DocumentProps.Title))
            {
                diagram.DocumentProps.Title = "My Diagram Title";
            }

            // Set the header text (centered) to the document title
            diagram.HeaderFooter.HeaderCenter = diagram.DocumentProps.Title;

            // Configure the header/footer font: Arial, 12 pt
            // Height uses a negative mapping: Height = -(PointSize * 1.333) ≈ -16 for 12 pt
            var headerFont = diagram.HeaderFooter.HeaderFooterFont;
            headerFont.FaceName = "Arial";
            headerFont.Height = -16;   // 12 pt font size
            headerFont.Weight = 700;   // Bold weight (optional)

            // Save the diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
    }