using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Add a new page to the diagram
        diagram.Pages.Add(new Page());
        Page page = diagram.Pages[0];

        // Create two rectangle shapes using DrawRectangle (returns shape IDs)
        long rectId1 = page.DrawRectangle(2.0, 2.0, 1.0, 0.5);
        long rectId2 = page.DrawRectangle(4.0, 2.0, 1.0, 0.5);

        // Retrieve the Shape objects from the IDs
        Shape rect1 = page.Shapes.GetShape(rectId1);
        Shape rect2 = page.Shapes.GetShape(rectId2);

        // Group the two rectangles into a single group shape
        Shape groupShape = page.Shapes.Group(new Shape[] { rect1, rect2 });

        // Iterate through each child shape in the group and apply a preset theme variant
        foreach (Shape child in groupShape.Shapes)
        {
            // Apply a preset theme (e.g., Bubble) and a variant (e.g., Variant1)
            child.PresetTheme = PresetThemeValue.Bubble;
            child.PresetThemeVariant = PresetThemeVariantValue.Variant1;
        }

        // Save the diagram to a VSDX file
        diagram.Save("GroupedWithTheme.vsdx", SaveFileFormat.Vsdx);

        Console.WriteLine("Diagram created, shapes grouped, and theme applied successfully.");
    }
}
