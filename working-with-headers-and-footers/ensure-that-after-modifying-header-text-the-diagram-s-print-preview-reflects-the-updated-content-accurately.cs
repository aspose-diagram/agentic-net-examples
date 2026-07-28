using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the updated Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Update the header text (centered)
            diagram.HeaderFooter.HeaderCenter = "Updated Header Text";

            // Optionally adjust header font properties
            var headerFont = diagram.HeaderFooter.HeaderFooterFont;
            headerFont.FaceName = "Arial";
            headerFont.Height = 12; // point size
            headerFont.Weight = 700; // bold

            // Save the diagram to persist header changes
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Print the diagram; the print preview will reflect the updated header
            diagram.Print();

            Console.WriteLine("Header updated, diagram saved, and sent to printer.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
