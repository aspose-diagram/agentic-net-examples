using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class UpdateHeaderCenter
{
    static void Main()
    {
        try
        {

            // Path to the source Visio diagram
            string inputPath = "input.vsdx";

            // Path where the modified diagram will be saved
            string outputPath = "output.vsdx";

            // Custom company name to set in the header center
            string companyName = "Acme Corp";

            // Load the diagram from file (using the Diagram(string) constructor)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Update the center portion of the document's header
                diagram.HeaderFooter.HeaderCenter = companyName;

                // Save the modified diagram back to a file (using VSDX format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
