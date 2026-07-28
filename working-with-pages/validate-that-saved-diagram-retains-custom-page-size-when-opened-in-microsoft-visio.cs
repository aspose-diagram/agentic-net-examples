using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define custom page size in inches (landscape 11"x8.5")
        double customWidth = 11.0;
        double customHeight = 8.5;

        // Path for the temporary VSDX file
        string filePath = "customPageSize.vsdx";

        try
        {
            // Create a new diagram – it already contains a default page (index 0)
            Diagram diagram = new Diagram();

            // Retrieve the existing first page instead of adding a new one
            Page page = diagram.Pages[0];

            // Apply the custom dimensions to the page
            page.PageSheet.PageProps.PageWidth.Value = customWidth;
            page.PageSheet.PageProps.PageHeight.Value = customHeight;

            // Save the diagram with the custom page size
            diagram.Save(filePath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram creation or saving: {ex.Message}");
            return;
        }

        // Verify that the file was created before attempting to load it
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found after save: {filePath}");
            return;
        }

        try
        {
            // Load the saved diagram
            Diagram loadedDiagram = new Diagram(filePath);

            // Ensure the diagram contains at least one page
            if (loadedDiagram.Pages.Count == 0)
                throw new Exception("No pages found in the loaded diagram.");

            // Retrieve the first (and only) page
            Page loadedPage = loadedDiagram.Pages[0];
            double loadedWidth = loadedPage.PageSheet.PageProps.PageWidth.Value;
            double loadedHeight = loadedPage.PageSheet.PageProps.PageHeight.Value;

            // Allow a tiny tolerance for floating‑point differences
            const double tolerance = 0.001;
            bool widthMatches = Math.Abs(loadedWidth - customWidth) <= tolerance;
            bool heightMatches = Math.Abs(loadedHeight - customHeight) <= tolerance;

            if (!widthMatches || !heightMatches)
                throw new Exception($"Page size mismatch. Expected ({customWidth}, {customHeight}) but got ({loadedWidth}, {loadedHeight}).");

            Console.WriteLine($"Page size retained correctly: width={loadedWidth}, height={loadedHeight}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram loading or validation: {ex.Message}");
        }
    }
}