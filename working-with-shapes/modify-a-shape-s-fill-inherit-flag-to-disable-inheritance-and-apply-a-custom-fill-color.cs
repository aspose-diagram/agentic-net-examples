using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Find the first non‑deleted shape on the page
            Shape targetShape = null;
            foreach (Shape shp in page.Shapes)
            {
                if (shp.Del == BOOL.False) // ensure the shape is not marked as deleted
                {
                    targetShape = shp;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine("No suitable shape found on the page.");
                return;
            }

            // Disable fill inheritance by setting a solid fill pattern
            targetShape.Fill.FillPattern.Value = 1; // 1 = solid fill

            // Apply a custom fill color (hex string)
            targetShape.Fill.FillForegnd.Value = "#FF5733";

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Shape fill updated and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
