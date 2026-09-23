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

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Detect pages with auto‑expand (DrawingResizeType) enabled
                    if (page.PageSheet.PageProps.DrawingResizeType.Value == DrawingResizeTypeValue.Automatically)
                    {
                        // Log original dimensions (in inches)
                        double originalWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double originalHeight = page.PageSheet.PageProps.PageHeight.Value;
                        Console.WriteLine($"Page ID {page.ID} ('{page.Name}') has auto‑expand enabled. Original size: {originalWidth}in x {originalHeight}in.");

                        // Example resizing: increase dimensions by 20%
                        double newWidth = originalWidth * 1.2;
                        double newHeight = originalHeight * 1.2;
                        page.PageSheet.PageProps.PageWidth.Value = newWidth;
                        page.PageSheet.PageProps.PageHeight.Value = newHeight;

                        Console.WriteLine($"Resized to: {newWidth}in x {newHeight}in.");
                    }
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
