using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class FooterUpdater
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Path where the modified Visio file will be saved
            string outputPath = "output.vsdx";

            // Load the diagram from the file (using the Diagram constructor that accepts a file path)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Clear any existing footer text
                diagram.HeaderFooter.FooterLeft = string.Empty;
                diagram.HeaderFooter.FooterCenter = string.Empty;
                diagram.HeaderFooter.FooterRight = string.Empty;

                // Standardized disclaimer to apply to all footer regions
                string disclaimer = "Confidential – For internal use only.";

                // Assign the disclaimer to each footer region
                diagram.HeaderFooter.FooterLeft = disclaimer;
                diagram.HeaderFooter.FooterCenter = disclaimer;
                diagram.HeaderFooter.FooterRight = disclaimer;

                // Save the modified diagram back to a file (using the Save method with VDX format)
                diagram.Save(outputPath, SaveFileFormat.Vdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
