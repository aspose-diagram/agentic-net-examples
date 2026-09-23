using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the modified Visio file
            string outputPath = "output.vsdx";

            // Load the diagram (ensure proper disposal)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define custom page dimensions (in inches)
                double customWidth = 11.0;   // example width
                double customHeight = 8.5;   // example height

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Check if auto‑expand (automatic page resizing) is enabled
                    bool isAutoExpand = page.PageSheet.PageProps.DrawingResizeType.Value == DrawingResizeTypeValue.Automatically;

                    if (isAutoExpand)
                    {
                        // Disable auto‑expand before applying manual size changes
                        page.PageSheet.PageProps.DrawingResizeType.Value = DrawingResizeTypeValue.NotAutomatically;
                    }

                    // Apply custom page size adjustments
                    page.PageSheet.PageProps.PageWidth.Value = customWidth;
                    page.PageSheet.PageProps.PageHeight.Value = customHeight;
                }

                // Save the modified diagram back to Visio format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
