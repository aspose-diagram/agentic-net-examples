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

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Process each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Remember the current auto‑expand setting
                DrawingResizeTypeValue originalResize = page.PageSheet.PageProps.DrawingResizeType.Value;

                // Temporarily disable auto‑expand
                page.PageSheet.PageProps.DrawingResizeType.Value = DrawingResizeTypeValue.NotAutomatically;

                // Example size modification: set width to 11 inches and height to 8.5 inches
                page.PageSheet.PageProps.PageWidth.Value = 11.0;
                page.PageSheet.PageProps.PageHeight.Value = 8.5;

                // Restore the original auto‑expand setting
                page.PageSheet.PageProps.DrawingResizeType.Value = originalResize;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
