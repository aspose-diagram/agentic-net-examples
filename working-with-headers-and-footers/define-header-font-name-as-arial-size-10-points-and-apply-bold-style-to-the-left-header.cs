using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Set the left header text
            diagram.HeaderFooter.HeaderLeft = "Sample Header";

            // Configure the header/footer font: Arial, 10 pt, bold
            // Height uses a negative value: Height = -(PointSize * 1.333) rounded
            // For 10 pt: -(10 * 1.333) ≈ -13
            diagram.HeaderFooter.HeaderFooterFont.FaceName = "Arial";
            diagram.HeaderFooter.HeaderFooterFont.Height = -13;   // 10 pt
            diagram.HeaderFooter.HeaderFooterFont.Weight = 700; // Bold (700)

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
