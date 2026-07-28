using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Paths for the original and modified Visio files
        string originalPath = "original.vsdx";
        string modifiedPath = "modified.vsdx";

        // Create a new diagram and add a page
        using (Diagram diagram = new Diagram())
        {
            // Add a blank page
            diagram.Pages.Add(new Page());

            // Access the first page
            Page page = diagram.Pages[0];

            // Set custom page size (e.g., A5 size)
            double newWidth = 5.83;   // inches
            double newHeight = 8.27;  // inches
            page.PageSheet.PageProps.PageWidth.Value = newWidth;
            page.PageSheet.PageProps.PageHeight.Value = newHeight;

            // Save the diagram with the modified page size
            diagram.Save(modifiedPath, SaveFileFormat.Vsdx);
        }

        // Reload the saved diagram to verify persistence
        using (Diagram loadedDiagram = new Diagram(modifiedPath))
        {
            // Retrieve the first page
            Page loadedPage = loadedDiagram.Pages[0];

            // Read the page dimensions
            double loadedWidth = loadedPage.PageSheet.PageProps.PageWidth.Value;
            double loadedHeight = loadedPage.PageSheet.PageProps.PageHeight.Value;

            // Expected dimensions (same as set earlier)
            double expectedWidth = 5.83;
            double expectedHeight = 8.27;

            // Validate width
            if (Math.Abs(loadedWidth - expectedWidth) > 0.001)
            {
                throw new Exception($"Page width mismatch. Expected: {expectedWidth}, Actual: {loadedWidth}");
            }

            // Validate height
            if (Math.Abs(loadedHeight - expectedHeight) > 0.001)
            {
                throw new Exception($"Page height mismatch. Expected: {expectedHeight}, Actual: {loadedHeight}");
            }

            Console.WriteLine("Page size changes persisted successfully after reload.");
        }
    }
}
