using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        const double customWidth = 5.0;   // inches
        const double customHeight = 7.0;  // inches
        const string filePath = "customPageSize.vsdx";

        // Create a new diagram, add a page, and set custom page dimensions
        using (Diagram diagram = new Diagram())
        {
            diagram.Pages.Add(new Page());                     // add a blank page
            Page page = diagram.Pages[0];                      // retrieve the first page
            page.PageSheet.PageProps.PageWidth.Value = customWidth;
            page.PageSheet.PageProps.PageHeight.Value = customHeight;

            // Save the diagram to a VSDX file
            diagram.Save(filePath, SaveFileFormat.Vsdx);
        }

        // Load the saved diagram and verify that the page size is preserved
        using (Diagram loadedDiagram = new Diagram(filePath))
        {
            Page loadedPage = loadedDiagram.Pages[0];
            double loadedWidth = loadedPage.PageSheet.PageProps.PageWidth.Value;
            double loadedHeight = loadedPage.PageSheet.PageProps.PageHeight.Value;

            const double tolerance = 0.001; // allow minor floating‑point differences
            if (Math.Abs(loadedWidth - customWidth) > tolerance ||
                Math.Abs(loadedHeight - customHeight) > tolerance)
            {
                throw new Exception(
                    $"Page size mismatch. Expected {customWidth}x{customHeight} inches, " +
                    $"but got {loadedWidth}x{loadedHeight} inches.");
            }

            Console.WriteLine($"Page size retained correctly: {loadedWidth} x {loadedHeight} inches.");
        }
    }
}
