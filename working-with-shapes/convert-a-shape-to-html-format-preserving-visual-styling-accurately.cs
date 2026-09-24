using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page
            Page page = diagram.Pages[0];

            // Find the first non‑deleted shape on the page
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.False)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine("No visible shape found on the page.");
                return;
            }

            // Export the shape to HTML while preserving its visual styling
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            targetShape.ToHTML("shape.html", htmlOptions);

            Console.WriteLine("Shape successfully exported to HTML.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
