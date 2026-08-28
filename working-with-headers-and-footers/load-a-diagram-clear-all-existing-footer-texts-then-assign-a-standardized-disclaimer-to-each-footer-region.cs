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

            // Input Visio file path
            string inputPath = "input.vsdx";

            // Output Visio file path
            string outputPath = "output.vsdx";

            // Load the diagram from the file (uses the Diagram(string) constructor)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Standard disclaimer to be placed in all footer sections
                string disclaimer = "Confidential: This document is for internal use only.";

                // Clear any existing footer text and assign the disclaimer
                diagram.HeaderFooter.FooterLeft = disclaimer;
                diagram.HeaderFooter.FooterCenter = disclaimer;
                diagram.HeaderFooter.FooterRight = disclaimer;

                // Save the modified diagram (uses the Diagram.Save method)
                diagram.Save(outputPath, SaveFileFormat.Vdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
