using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Define temporary file path for the Visio document
        string tempFile = Path.Combine(Path.GetTempPath(), "PageSizeTest.vsdx");

        // Desired page dimensions (in inches)
        double targetWidth = 8.27;   // A4 width
        double targetHeight = 11.69; // A4 height

        // -----------------------------------------------------------------
        // Create a new diagram, add a page, set its size, and save it
        // -----------------------------------------------------------------
        using (var diagram = new Diagram())
        {
            // Add a blank page to the diagram
            diagram.Pages.Add(new Page());

            // Access the first (and only) page
            Page page = diagram.Pages[0];

            // Set page width and height
            page.PageSheet.PageProps.PageWidth.Value = targetWidth;
            page.PageSheet.PageProps.PageHeight.Value = targetHeight;

            // Save the diagram to a VSDX file
            diagram.Save(tempFile, SaveFileFormat.Vsdx);
        }

        // -----------------------------------------------------------------
        // Reload the saved diagram and verify that the page size persisted
        // -----------------------------------------------------------------
        using (var loadedDiagram = new Diagram(tempFile))
        {
            // Access the first page
            Page loadedPage = loadedDiagram.Pages[0];

            // Retrieve saved dimensions
            double savedWidth = loadedPage.PageSheet.PageProps.PageWidth.Value;
            double savedHeight = loadedPage.PageSheet.PageProps.PageHeight.Value;

            // Define a tolerance for floating‑point comparison
            const double tolerance = 0.001;

            // Validate width
            if (Math.Abs(savedWidth - targetWidth) > tolerance)
            {
                throw new Exception($"Page width mismatch. Expected: {targetWidth}, Actual: {savedWidth}");
            }

            // Validate height
            if (Math.Abs(savedHeight - targetHeight) > tolerance)
            {
                throw new Exception($"Page height mismatch. Expected: {targetHeight}, Actual: {savedHeight}");
            }

            Console.WriteLine("Page size persisted correctly after reload.");
        }

        // Clean up temporary file (optional)
        try
        {
            File.Delete(tempFile);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}
