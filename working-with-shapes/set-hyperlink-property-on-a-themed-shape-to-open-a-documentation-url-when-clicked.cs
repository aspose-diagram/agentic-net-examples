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

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Define shape geometry and master name
            double pinX = 5.0;      // X coordinate (in inches)
            double pinY = 5.0;      // Y coordinate (in inches)
            double width = 2.0;     // Shape width (in inches)
            double height = 1.0;    // Shape height (in inches)
            string masterName = "Rectangle";

            // Add the shape to the page; AddShape returns the shape ID (long)
            long shapeId = page.AddShape(pinX, pinY, width, height, masterName, false);

            // Retrieve the concrete Shape object using the returned ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Apply a preset theme to the shape (write‑only properties)
            shape.PresetTheme = PresetThemeValue.Bubble;
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

            // Create a hyperlink that points to the documentation URL
            Hyperlink link = new Hyperlink();
            link.Name = "DocLink";
            link.Address.Value = "https://example.com/documentation";
            link.Description.Value = "Open documentation";

            // Add the hyperlink to the shape's Hyperlinks collection
            shape.Hyperlinks.Add(link);

            // Save the diagram to VSDX format
            string outputPath = "ThemedShapeWithLink.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
