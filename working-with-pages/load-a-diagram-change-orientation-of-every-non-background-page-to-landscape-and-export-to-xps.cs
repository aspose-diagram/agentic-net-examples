using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";

            // Output XPS file path
            string outputPath = "output.xps";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Set orientation of every non‑background page to Landscape
            foreach (Page page in diagram.Pages)
            {
                if (page.Background != BOOL.True)
                {
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                }
            }

            // Configure XPS save options (do not export hidden pages)
            XPSSaveOptions options = new XPSSaveOptions();
            options.ExportHiddenPage = false;

            // Save the diagram as XPS
            diagram.Save(outputPath, options);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
