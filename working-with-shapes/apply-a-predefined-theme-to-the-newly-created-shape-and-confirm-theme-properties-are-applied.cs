using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "ThemedShape.vsdx";

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first page (avoid using ActivePage)
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page at position (2,2)
            // Use the overload that takes pinX, pinY, width, height, master name
            long shapeId = page.AddShape(2.0, 2.0, 1.0, 0.5, "Rectangle", false);

            // Retrieve the shape instance using the returned ID (cast to int for GetShape)
            Shape shape = page.Shapes.GetShape((int)shapeId);

            // Apply a predefined theme to the shape
            shape.PresetTheme = PresetThemeValue.Bubble;
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

            // Save the diagram with a valid SaveFileFormat argument
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Confirmation output
            Console.WriteLine($"Shape with ID {shapeId} has been themed and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}