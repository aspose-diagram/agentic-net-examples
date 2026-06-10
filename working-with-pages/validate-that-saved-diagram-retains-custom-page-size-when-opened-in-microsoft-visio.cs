using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Define file paths
            string outputPath = "CustomPageSizeDiagram.vsdx";

            // Custom page size in inches
            double customWidth = 11.0;   // e.g., 11 inches
            double customHeight = 8.5;   // e.g., 8.5 inches

            // -------------------------------------------------
            // 1. Create a new diagram and set custom page size
            // -------------------------------------------------
            Diagram diagram = new Diagram(); // empty diagram

            // Ensure there is at least one page (default diagram has one)
            Page page = diagram.Pages[0];

            // Set the page dimensions via PageSheet.PageProps
            page.PageSheet.PageProps.PageWidth.Value = customWidth;
            page.PageSheet.PageProps.PageHeight.Value = customHeight;

            // Save the diagram to a VSDX file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // -------------------------------------------------
            // 2. Load the saved diagram and verify page size
            // -------------------------------------------------
            Diagram loadedDiagram = new Diagram(outputPath);

            Page loadedPage = loadedDiagram.Pages[0];

            double loadedWidth = loadedPage.PageSheet.PageProps.PageWidth.Value;
            double loadedHeight = loadedPage.PageSheet.PageProps.PageHeight.Value;

            // Allow a tiny tolerance for floating‑point differences
            const double tolerance = 0.0001;

            if (Math.Abs(loadedWidth - customWidth) > tolerance ||
                Math.Abs(loadedHeight - customHeight) > tolerance)
            {
                throw new Exception($"Page size validation failed. Expected ({customWidth} x {customHeight}) inches, but got ({loadedWidth} x {loadedHeight}) inches.");
            }

            Console.WriteLine("Page size validation succeeded. Custom dimensions are retained after saving and loading.");
        }
    }