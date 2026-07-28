using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file (VDX, VSDX, etc.)
            string visioPath = @"C:\Path\To\YourDiagram.vsdx";

            // Load the diagram using the appropriate constructor.
            // The constructor automatically detects the format based on the file extension.
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Iterate through all pages in the document.
                foreach (Page page in diagram.Pages)
                {
                    Console.WriteLine($"Page: {page.Name} (ID: {page.ID})");

                    // Recursively enumerate shapes on the page.
                    EnumerateShapes(page.Shapes);
                }
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }

    // Recursively checks each shape and its child shapes for an embedded ActiveX control.
    static void EnumerateShapes(ShapeCollection shapes)
    {
        foreach (Shape shape in shapes)
        {
            // If the shape contains an ActiveX control, the property will be non‑null.
            if (shape.ActiveXControl != null)
            {
                // Output basic information about the shape and its control.
                Console.WriteLine($"  Shape ID: {shape.ID}, Name: {shape.Name}");
                Console.WriteLine($"    ActiveX Control Type: {shape.ActiveXControl.Type}");
                // Additional properties can be accessed, e.g., Width, Height, IsEnabled, etc.
                Console.WriteLine($"    Size (pts): {shape.ActiveXControl.Width} x {shape.ActiveXControl.Height}");
                Console.WriteLine($"    Enabled: {shape.ActiveXControl.IsEnabled}");
            }

            // If the shape is a group, it may contain nested shapes.
            if (shape.Shapes != null && shape.Shapes.Count > 0)
            {
                EnumerateShapes(shape.Shapes);
            }
        }
    }
}
