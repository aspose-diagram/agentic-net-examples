using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for the original and modified Visio files
            string inputPath = "input.vsdx";
            string outputPath = "modified.vsdx";

            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                throw new Exception("The diagram contains no pages.");
            }

            // Select the first page
            Page page = diagram.Pages[0];

            // Desired page size (A4 in inches)
            double newWidth = 8.27;
            double newHeight = 11.69;

            // Apply the new size
            page.PageSheet.PageProps.PageWidth.Value = newWidth;
            page.PageSheet.PageProps.PageHeight.Value = newHeight;

            // Save the diagram with the updated page size
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Reload the saved diagram to verify persistence
            Diagram reloadedDiagram = new Diagram(outputPath);

            if (reloadedDiagram.Pages.Count == 0)
            {
                throw new Exception("The reloaded diagram contains no pages.");
            }

            Page reloadedPage = reloadedDiagram.Pages[0];
            double loadedWidth = reloadedPage.PageSheet.PageProps.PageWidth.Value;
            double loadedHeight = reloadedPage.PageSheet.PageProps.PageHeight.Value;

            // Validate that the page size persisted
            const double tolerance = 0.001; // tolerance for floating‑point comparison
            if (Math.Abs(loadedWidth - newWidth) > tolerance || Math.Abs(loadedHeight - newHeight) > tolerance)
            {
                throw new Exception($"Page size did not persist. Expected ({newWidth}, {newHeight}) but got ({loadedWidth}, {loadedHeight}).");
            }

            Console.WriteLine($"Page size persisted correctly: Width = {loadedWidth}, Height = {loadedHeight}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
