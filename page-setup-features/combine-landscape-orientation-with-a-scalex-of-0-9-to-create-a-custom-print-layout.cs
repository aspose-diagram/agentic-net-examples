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

            // Iterate through all pages and set custom print layout
            foreach (Page page in diagram.Pages)
            {
                // Access the print properties of the page
                PrintProps printProps = page.PageSheet.PrintProps;

                // Set orientation to Landscape
                printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                // Set horizontal scaling factor to 90%
                printProps.ScaleX.Value = 0.9;
            }

            // Save the modified diagram
            string outputPath = "output_custom_print.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up
            diagram.Dispose();

            Console.WriteLine("Custom print layout applied and diagram saved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
