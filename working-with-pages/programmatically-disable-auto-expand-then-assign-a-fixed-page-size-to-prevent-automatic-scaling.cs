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

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the modified Visio file
            string outputPath = "output_fixed_page.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Fixed page dimensions (A4 size in inches)
            double fixedWidth = 8.27;   // inches
            double fixedHeight = 11.69; // inches

            // Iterate through all pages and apply settings
            foreach (Page page in diagram.Pages)
            {
                // Disable auto‑expand (automatic drawing resize)
                page.PageSheet.PageProps.DrawingResizeType.Value = DrawingResizeTypeValue.NotAutomatically;

                // Set fixed page width and height
                page.PageSheet.PageProps.PageWidth.Value = fixedWidth;
                page.PageSheet.PageProps.PageHeight.Value = fixedHeight;
            }

            // Save the modified diagram back to Visio format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
