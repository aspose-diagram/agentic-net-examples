using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file (must exist)
            string inputPath = "input.vsdx";

            // Output file in VDX format
            string outputPath = "output.vdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify that a second page exists
                if (diagram.Pages.Count < 2)
                {
                    Console.WriteLine("The diagram does not contain a second page.");
                    return;
                }

                // Retrieve the second page (index 1)
                Page secondPage = diagram.Pages[1];

                // Set the page height to 11 inches
                secondPage.PageSheet.PageProps.PageHeight.Value = 11.0;

                // Export the diagram to VDX format
                diagram.Save(outputPath, SaveFileFormat.Vdx);
            }

            Console.WriteLine("Diagram saved as VDX successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
