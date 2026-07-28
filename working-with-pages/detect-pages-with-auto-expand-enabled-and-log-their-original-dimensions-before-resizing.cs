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
            string outputPath = "output_resized.vsdx";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Detect if the page has auto‑expand (DrawingResizeType) enabled
                    if (page.PageSheet.PageProps.DrawingResizeType.Value == DrawingResizeTypeValue.Automatically)
                    {
                        // Log original dimensions
                        double originalWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double originalHeight = page.PageSheet.PageProps.PageHeight.Value;
                        Console.WriteLine($"Page ID {page.ID} ('{page.Name}') has auto‑expand enabled.");
                        Console.WriteLine($"Original size: Width = {originalWidth} in, Height = {originalHeight} in.");

                        // Resize the page (example: set to 11 x 8.5 inches)
                        page.PageSheet.PageProps.PageWidth.Value = 11.0;
                        page.PageSheet.PageProps.PageHeight.Value = 8.5;

                        // Optionally turn off auto‑expand after resizing
                        page.PageSheet.PageProps.DrawingResizeType.Value = DrawingResizeTypeValue.NotAutomatically;
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
