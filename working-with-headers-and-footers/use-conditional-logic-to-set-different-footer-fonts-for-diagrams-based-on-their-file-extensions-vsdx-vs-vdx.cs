using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Determine file extension
        string extension = Path.GetExtension(inputPath).ToLowerInvariant();

        Diagram diagram;

        if (extension == ".vsdx")
        {
            // Load VSDX file
            diagram = new Diagram(inputPath, LoadFileFormat.Vsdx);

            // Set footer font for VSDX diagrams
            diagram.HeaderFooter.HeaderFooterFont.FaceName = "Calibri";
            diagram.HeaderFooter.HeaderFooterFont.Weight = 700;   // Bold
            diagram.HeaderFooter.HeaderFooterFont.Height = -16;   // Approx. 12pt
        }
        else if (extension == ".vdx")
        {
            // Load VDX file
            diagram = new Diagram(inputPath, LoadFileFormat.Vdx);

            // Set footer font for VDX diagrams
            diagram.HeaderFooter.HeaderFooterFont.FaceName = "Arial";
            diagram.HeaderFooter.HeaderFooterFont.Weight = 400;   // Regular
            diagram.HeaderFooter.HeaderFooterFont.Height = -12;   // Approx. 9pt
        }
        else
        {
            Console.WriteLine("Unsupported file format. Only .vsdx and .vdx are supported.");
            return;
        }

        // Save the updated diagram with a new name
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_updated" + extension);

        if (extension == ".vsdx")
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        else
            diagram.Save(outputPath, SaveFileFormat.Vdx);

        Console.WriteLine($"Diagram saved to: {outputPath}");
    }
}
