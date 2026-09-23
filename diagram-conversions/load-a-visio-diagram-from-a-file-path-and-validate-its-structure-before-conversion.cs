using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Get the input file path from command‑line arguments or use a default placeholder.
        string inputPath = args.Length > 0 ? args[0] : "sample.vsdx";

        // Load the Visio diagram.
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // ---------- Structure Validation ----------

        // 1. Ensure the diagram contains at least one page.
        if (diagram.Pages == null || diagram.Pages.Count == 0)
            throw new Exception("The diagram contains no pages.");

        // 2. Iterate through each page and validate its properties.
        foreach (Page page in diagram.Pages)
        {
            // Validate page dimensions (must be positive).
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;
            if (pageWidth <= 0 || pageHeight <= 0)
                throw new Exception($"Page \"{page.Name}\" has invalid dimensions (Width={pageWidth}, Height={pageHeight}).");

            // Validate that the page has a shape collection.
            if (page.Shapes == null || page.Shapes.Count == 0)
                Console.WriteLine($"Warning: Page \"{page.Name}\" contains no shapes.");

            // 3. Validate each shape on the page.
            foreach (Shape shape in page.Shapes)
            {
                // Skip shapes that are marked as deleted.
                if (shape.Del == BOOL.True)
                    continue;

                // For non‑foreign shapes, ensure a master is associated.
                if (shape.Type != TypeValue.Foreign && shape.Master == null)
                    throw new Exception($"Shape ID {shape.ID} on page \"{page.Name}\" lacks a master.");

                // Example check: report shapes with empty text.
                string shapeText = shape.Text.Value.ToString();
                if (string.IsNullOrWhiteSpace(shapeText))
                    Console.WriteLine($"Info: Shape ID {shape.ID} on page \"{page.Name}\" has empty text.");
            }
        }

        // 4. Validate basic document properties.
        if (string.IsNullOrWhiteSpace(diagram.DocumentProps.Title))
            Console.WriteLine("Notice: Document title property is empty.");

        // If all checks pass, report success.
        Console.WriteLine("Diagram validation completed successfully.");
    }
}
