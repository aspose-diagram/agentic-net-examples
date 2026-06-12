using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Assume the first page contains the group shape
            Page page = diagram.Pages[0];

            // Find the first group shape on the page
            Shape? groupShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Type == TypeValue.Group)
                {
                    groupShape = shape;
                    break;
                }
            }

            if (groupShape == null)
            {
                throw new Exception("No group shape found on the page.");
            }

            // Iterate through child shapes of the group and apply a preset theme variant
            foreach (Shape child in groupShape.Shapes)
            {
                // Apply the desired theme and variant to each child shape
                child.PresetTheme = PresetThemeValue.Bubble;
                child.PresetThemeVariant = PresetThemeVariantValue.Variant2;
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
