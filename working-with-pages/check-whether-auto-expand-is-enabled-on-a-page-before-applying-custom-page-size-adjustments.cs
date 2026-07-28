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
            string outputPath = "output_modified.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Check if auto‑expand (automatic page resizing) is enabled
                    bool isAutoExpandEnabled = page.PageSheet.PageProps.DrawingResizeType.Value == DrawingResizeTypeValue.Automatically;

                    if (isAutoExpandEnabled)
                    {
                        Console.WriteLine($"Page \"{page.Name}\" (ID: {page.ID}) has auto‑expand enabled. Applying custom size.");

                        // Example custom size: A4 dimensions in inches
                        double customWidth = 8.27;   // inches
                        double customHeight = 11.69; // inches

                        page.PageSheet.PageProps.PageWidth.Value = customWidth;
                        page.PageSheet.PageProps.PageHeight.Value = customHeight;
                    }
                    else
                    {
                        Console.WriteLine($"Page \"{page.Name}\" (ID: {page.ID}) does NOT have auto‑expand enabled. Skipping size adjustment.");
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to \"{outputPath}\".");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
