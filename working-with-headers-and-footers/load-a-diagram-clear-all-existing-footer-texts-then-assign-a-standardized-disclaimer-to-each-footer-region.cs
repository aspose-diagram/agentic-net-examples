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

            // Load the existing Visio diagram
            var diagram = new Diagram("input.vsdx");

            // Clear any existing footer text
            diagram.HeaderFooter.FooterLeft = string.Empty;
            diagram.HeaderFooter.FooterCenter = string.Empty;
            diagram.HeaderFooter.FooterRight = string.Empty;

            // Standardized disclaimer to be applied
            const string disclaimer = "Confidential – For internal use only";

            // Assign the disclaimer to each footer region
            diagram.HeaderFooter.FooterLeft = disclaimer;
            diagram.HeaderFooter.FooterCenter = disclaimer;
            diagram.HeaderFooter.FooterRight = disclaimer;

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
