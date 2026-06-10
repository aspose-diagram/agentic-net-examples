using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the source diagram containing the template page and target pages
            Diagram diagram = new Diagram("input.vsdx");

            // Index of the template page whose size will be copied (e.g., first page)
            int templatePageIndex = 0;
            Page templatePage = diagram.Pages[templatePageIndex];
            PageProps templateProps = templatePage.PageSheet.PageProps;

            // Indices of pages that should receive the template size
            int[] targetPageIndices = new int[] { 1, 2, 3 }; // adjust as needed

            foreach (int idx in targetPageIndices)
            {
                Page targetPage = diagram.Pages[idx];
                PageProps targetProps = targetPage.PageSheet.PageProps;

                // Copy page width and height from the template
                targetProps.PageWidth.Value = templateProps.PageWidth.Value;
                targetProps.PageHeight.Value = templateProps.PageHeight.Value;

                // Optionally copy related scale properties to keep drawing consistency
                targetProps.PageScale.Value = templateProps.PageScale.Value;
                targetProps.DrawingScale.Value = templateProps.DrawingScale.Value;
            }

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
