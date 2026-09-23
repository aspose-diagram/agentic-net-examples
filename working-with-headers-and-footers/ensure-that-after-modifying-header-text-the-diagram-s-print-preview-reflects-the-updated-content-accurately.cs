using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Update header text sections
            diagram.HeaderFooter.HeaderLeft = "Company Name";
            diagram.HeaderFooter.HeaderCenter = "Quarterly Report";
            diagram.HeaderFooter.HeaderRight = "Date: &d";

            // Adjust header font appearance
            var headerFont = diagram.HeaderFooter.HeaderFooterFont;
            headerFont.FaceName = "Arial";
            headerFont.Height = 12;      // Font size in points (int)
            headerFont.Weight = 700;    // 700 = bold, 400 = regular

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Header text updated. Saved diagram to '" + outputPath + "'. Print preview will reflect the new header.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
