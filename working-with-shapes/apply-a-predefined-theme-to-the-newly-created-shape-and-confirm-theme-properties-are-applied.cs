using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new blank diagram
        Diagram diagram = new Diagram();

        // Access the first page (a new diagram always contains at least one page)
        Page page = diagram.Pages[0];

        // Define rectangle geometry
        double pinX = 2.0;   // X coordinate of the shape's center
        double pinY = 2.0;   // Y coordinate of the shape's center
        double width = 2.0;  // Width of the rectangle
        double height = 1.0; // Height of the rectangle

        // Add a rectangle shape to the page
        long shapeId = page.DrawRectangle(pinX, pinY, width, height);

        // Retrieve the Shape object using the returned ID
        Shape shape = page.Shapes.GetShape((int)shapeId);

        // Apply a predefined theme to the shape
        shape.PresetTheme = PresetThemeValue.Bubble;
        shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
        shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

        // Log the applied theme settings (properties are write‑only, so we log the values we set)
        Console.WriteLine($"Applied theme to shape ID {shape.ID}");
        Console.WriteLine("PresetTheme set to Bubble");
        Console.WriteLine("PresetThemeVariant set to Variant1");
        Console.WriteLine("PresetThemeQuickStyle set to VariantStyle1");

        // Save the diagram to VSDX format
        diagram.Save("ThemedShape.vsdx", SaveFileFormat.Vsdx);
        Console.WriteLine("Diagram saved as ThemedShape.vsdx");
    }
}
