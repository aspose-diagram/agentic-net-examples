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

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output XPS file path
            string outputPath = "output.xps";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Change orientation to Landscape for every non‑background page
            foreach (Page page in diagram.Pages)
            {
                if (page.Background == BOOL.False)
                {
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                }
            }

            // Export the diagram to XPS
            XPSSaveOptions options = new XPSSaveOptions();
            options.ExportHiddenPage = false; // export only foreground pages
            diagram.Save(outputPath, options);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
