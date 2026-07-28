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

            // Load the source diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Index of the template page (0‑based)
            int templatePageIndex = 0;
            Page templatePage = diagram.Pages[templatePageIndex];

            // Retrieve width and height from the template page's PageProps
            double templateWidth = Convert.ToDouble(templatePage.PageSheet.PageProps.PageWidth.Value);
            double templateHeight = Convert.ToDouble(templatePage.PageSheet.PageProps.PageHeight.Value);

            // Define the target pages that should receive the same size
            int[] targetPageIndices = { 1, 2, 3 }; // example indices

            // Apply the template size to each target page
            foreach (int idx in targetPageIndices)
            {
                Page targetPage = diagram.Pages[idx];
                targetPage.PageSheet.PageProps.PageWidth.Value = templateWidth;
                targetPage.PageSheet.PageProps.PageHeight.Value = templateHeight;
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
