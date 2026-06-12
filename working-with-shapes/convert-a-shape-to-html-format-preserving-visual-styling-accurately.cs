using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ShapeToHtmlConverter
{
    // Converts a specific shape from a Visio diagram to an HTML file preserving its visual styling.
    public static void ConvertShapeToHtml(string diagramPath, int shapeId, string outputHtmlPath)
    {
        // Load the Visio diagram from the specified file.
        Diagram diagram = new Diagram(diagramPath);

        // Find the shape with the given ID across all pages.
        Shape targetShape = null;
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                if (shape.ID == shapeId)
                {
                    targetShape = shape;
                    break;
                }
            }
            if (targetShape != null) break;
        }

        if (targetShape == null)
            throw new ArgumentException($"Shape with ID {shapeId} not found in the diagram.");

        // Configure HTML save options to preserve styling.
        HTMLSaveOptions htmlOptions = new HTMLSaveOptions
        {
            // Export all shapes (including hidden) to keep visual fidelity.
            ExportHiddenPage = true,
            // Keep the original page size for accurate rendering.
            PageSize = null,
            // Use a high resolution for better quality.
            Resolution = 300,
            // Export guide shapes if they are part of the visual layout.
            ExportGuideShapes = true,
            // Save as a single HTML file (optional, can be set to false for multiple files).
            SaveAsSingleFile = true
        };

        // Save the shape as HTML using the built‑in ToHTML method.
        targetShape.ToHTML(outputHtmlPath, htmlOptions);
    }

    // Example usage.
    static void Main()
    {
        try
        {

            string diagramFile = @"C:\Docs\SampleDiagram.vsdx";
            int shapeId = 5; // Replace with the actual shape ID you want to export.
            string htmlOutput = @"C:\Docs\Shape5.html";

            ConvertShapeToHtml(diagramFile, shapeId, htmlOutput);

            Console.WriteLine("Shape exported to HTML successfully.");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
