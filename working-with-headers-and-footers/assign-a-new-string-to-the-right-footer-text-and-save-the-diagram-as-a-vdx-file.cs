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

            // Path to the source diagram file
            string inputPath = "input.vsdx"; // modify as needed

            // Path for the output VDX file
            string outputPath = "output.vdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Assign a new string to the right footer
            diagram.HeaderFooter.FooterRight = "Confidential - Page &p of &P";

            // Save the diagram in VDX format
            diagram.Save(outputPath, SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
