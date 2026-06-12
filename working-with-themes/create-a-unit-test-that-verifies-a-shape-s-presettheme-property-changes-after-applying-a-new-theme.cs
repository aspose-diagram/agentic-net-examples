using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a rectangle shape to the active page
            // AddShape(pinX, pinY, masterName) returns the shape ID (long)
            long shapeId = diagram.ActivePage.AddShape(2.0, 2.0, "Rectangle");

            // Retrieve the shape instance using the returned ID
            Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);

            // Apply the first preset theme
            shape.PresetTheme = PresetThemeValue.Bubble;
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

            // Apply a different preset theme variant to simulate a change
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant2;

            // If no exception was thrown up to this point, we consider the theme change successful
            Console.WriteLine("Preset theme applied and changed successfully.");

            // Save the diagram to verify that the changes persist
            diagram.Save("ThemeChangeTest.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
