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

            // Load the Visio diagram from a file
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Reduce the print scaling to 75% on every page
            foreach (Page page in diagram.Pages)
            {
                // ScaleX controls the horizontal scaling factor (1.0 = 100%)
                page.PageSheet.PrintProps.ScaleX.Value = 0.75;
            }

            // Save the modified diagram back to Visio format
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up resources
            diagram.Dispose();

            Console.WriteLine("Print scaling set to 75% and diagram saved to " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
