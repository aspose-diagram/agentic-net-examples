using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio diagram file.
        string diagramPath = "input.vsdx";

        // Verify that the diagram file exists before proceeding.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(diagramPath);

            // Use LINQ to collect shapes that inherit both fill and line formatting from their masters.
            var inheritedShapes =
                from page in diagram.Pages.Cast<Page>()                     // Enumerate all pages.
                from shape in page.Shapes.Cast<Shape>()                     // Enumerate all shapes on each page.
                // Exclude deleted shapes.
                where shape.Del == BOOL.False
                // Ensure both InheritFill and InheritLine are available.
                where shape.InheritFill != null && shape.InheritLine != null
                // Compare a representative fill property (foreground color) with its inherited value.
                where shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value
                // Compare a representative line property (line color) with its inherited value.
                where shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value
                // Project both the shape and its page index for later reporting.
                select new { Shape = shape, PageIndex = diagram.Pages.IndexOf(page) };

            // Output the results.
            Console.WriteLine("Shapes inheriting both fill and line formatting from masters:");
            foreach (var item in inheritedShapes)
            {
                // Retrieve shape details and its page index.
                Console.WriteLine($"Page {item.PageIndex + 1}, Shape ID: {item.Shape.ID}, NameU: {item.Shape.NameU}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}