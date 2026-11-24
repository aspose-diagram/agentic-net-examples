using System;
using System.IO;
using Aspose.Diagram;

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

            // Add a new page to the diagram
            // The Add method returns the newly created Page object
            Page page = new Page();
            diagram.Pages.Add(page);

            // Define shape position (in inches) and master name
            double pinX = 2.0;
            double pinY = 2.0;
            string masterName = "Rectangle"; // Using a built‑in master shape

            // Add the shape to the page; the method returns the shape ID (long)
            long shapeId = page.AddShape(pinX, pinY, masterName, false);

            // Retrieve the Shape instance using the returned ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Apply a predefined theme to the shape
            // These properties are write‑only; they cannot be read back
            shape.PresetTheme = PresetThemeValue.Bubble;
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

            // Save the diagram to verify that the theme was applied
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Confirmation message
            Console.WriteLine($"Diagram saved to '{outputPath}'. Theme applied to shape ID {shapeId}.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}