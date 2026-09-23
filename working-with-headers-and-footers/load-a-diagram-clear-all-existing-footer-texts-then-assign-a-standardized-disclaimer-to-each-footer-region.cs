using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Path where the modified file will be saved
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Clear any existing footer text
            diagram.HeaderFooter.FooterLeft = "";
            diagram.HeaderFooter.FooterCenter = "";
            diagram.HeaderFooter.FooterRight = "";

            // Standardized disclaimer to apply to all footer regions
            string disclaimer = "Confidential - Do not distribute";

            // Assign the disclaimer to each footer region
            diagram.HeaderFooter.FooterLeft = disclaimer;
            diagram.HeaderFooter.FooterCenter = disclaimer;
            diagram.HeaderFooter.FooterRight = disclaimer;

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
