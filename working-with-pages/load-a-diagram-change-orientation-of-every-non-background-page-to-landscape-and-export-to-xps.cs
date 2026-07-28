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

            // Path to the input Visio file
            string inputPath = "input.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and set orientation to Landscape for non‑background pages
            foreach (Page page in diagram.Pages)
            {
                // Skip background pages
                if (page.Background == BOOL.False)
                {
                    // Set print orientation to Landscape
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                }
            }

            // Configure XPS save options (optional settings can be adjusted here)
            XPSSaveOptions xpsOptions = new XPSSaveOptions();
            xpsOptions.ExportHiddenPage = false; // do not export hidden pages

            // Path to the output XPS file
            string outputPath = "output.xps";

            // Save the diagram as XPS using the configured options
            diagram.Save(outputPath, xpsOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
