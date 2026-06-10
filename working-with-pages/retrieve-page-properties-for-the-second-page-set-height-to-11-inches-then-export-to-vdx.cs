using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vdx";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify that a second page exists
                if (diagram.Pages.Count < 2)
                    throw new Exception("The diagram does not contain a second page.");

                // Retrieve the second page (index 1)
                Page secondPage = diagram.Pages[1];

                // Set the page height to 11 inches
                secondPage.PageSheet.PageProps.PageHeight.Value = 11.0;

                // Export the diagram to VDX format
                diagram.Save(outputPath, SaveFileFormat.Vdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
