using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Apply custom print layout to each page
            foreach (Page page in diagram.Pages)
            {
                // Set orientation to Landscape
                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                // Set horizontal scaling to 90%
                page.PageSheet.PrintProps.ScaleX.Value = 0.9;
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up resources
            diagram.Dispose();

            Console.WriteLine("Custom print layout applied and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
