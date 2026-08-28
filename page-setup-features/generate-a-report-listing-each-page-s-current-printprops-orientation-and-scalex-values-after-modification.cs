using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Modify PrintProps for each page
            foreach (Page page in diagram.Pages)
            {
                // Set orientation to Landscape
                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                // Set horizontal scaling factor to 75%
                page.PageSheet.PrintProps.ScaleX.Value = 0.75;
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Generate a report of orientation and ScaleX for each page
            foreach (Page page in diagram.Pages)
            {
                PrintPageOrientationValue orientation = page.PageSheet.PrintProps.PrintPageOrientation.Value;
                double scaleX = page.PageSheet.PrintProps.ScaleX.Value;

                Console.WriteLine($"Page '{page.Name}' - Orientation: {orientation}, ScaleX: {scaleX}");
            }

            // Dispose the diagram to release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
