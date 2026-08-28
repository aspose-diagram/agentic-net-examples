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

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Store original auto‑expand setting
                    var originalResizeType = page.PageSheet.PageProps.DrawingResizeType.Value;

                    // Temporarily disable auto‑expand
                    page.PageSheet.PageProps.DrawingResizeType.Value = DrawingResizeTypeValue.NotAutomatically;

                    // Modify page size (example: set to 11" x 8.5")
                    page.PageSheet.PageProps.PageWidth.Value = 11.0;   // width in inches
                    page.PageSheet.PageProps.PageHeight.Value = 8.5;   // height in inches

                    // Re‑enable auto‑expand by restoring original value
                    page.PageSheet.PageProps.DrawingResizeType.Value = originalResizeType;
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
