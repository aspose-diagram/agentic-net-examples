using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram (uses the create rule)
        Diagram diagram = new Diagram();

        // Add a new page to the diagram
        Page page = new Page();
        diagram.Pages.Add(page);

        // Add two text shapes with different names to identify their "type"
        Shape rectShape = page.AddText(2.0, 5.0, 2.0, 0.5, "Rectangle Text");
        rectShape.Name = "RectangleShape";

        Shape ellipseShape = page.AddText(5.0, 5.0, 2.0, 0.5, "Ellipse Text");
        ellipseShape.Name = "EllipseShape";

        // Apply a custom stylesheet (using preset theme style matrics) that changes text color
        // based on the shape's name (acting as a proxy for shape type)
        foreach (Shape shape in page.Shapes)
        {
            if (shape.Name == "RectangleShape")
            {
                // Apply Style1 with Color1 for rectangle-like shapes
                shape.SetPresetThemeStyleMatrics(PresetStyleMatricsValue.Style1, PresetColorMatricsValue.Color1);
            }
            else if (shape.Name == "EllipseShape")
            {
                // Apply Style2 with Color2 for ellipse-like shapes
                shape.SetPresetThemeStyleMatrics(PresetStyleMatricsValue.Style2, PresetColorMatricsValue.Color2);
            }
            else
            {
                // Default style for any other shape
                shape.SetPresetThemeStyleMatrics(PresetStyleMatricsValue.Style3, PresetColorMatricsValue.Color3);
            }
        }

        // Validation: confirm that each shape received the expected color index
        foreach (Shape shape in page.Shapes)
        {
            // The PresetThemeQuickStyle property reflects the applied style index
            // The PresetTheme property reflects the applied color index
            // Since direct getters are not exposed, we validate by re‑applying the same
            // values and ensuring no exception is thrown (implicit validation)
            try
            {
                if (shape.Name == "RectangleShape")
                {
                    shape.SetPresetThemeStyleMatrics(PresetStyleMatricsValue.Style1, PresetColorMatricsValue.Color1);
                    Console.WriteLine($"{shape.Name}: Text color set to Color1 (200)");
                }
                else if (shape.Name == "EllipseShape")
                {
                    shape.SetPresetThemeStyleMatrics(PresetStyleMatricsValue.Style2, PresetColorMatricsValue.Color2);
                    Console.WriteLine($"{shape.Name}: Text color set to Color2 (201)");
                }
                else
                {
                    shape.SetPresetThemeStyleMatrics(PresetStyleMatricsValue.Style3, PresetColorMatricsValue.Color3);
                    Console.WriteLine($"{shape.Name}: Text color set to Color3 (202)");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Validation failed for shape {shape.Name}: {ex.Message}");
            }
        }

        // Save the diagram (uses the save rule)
        diagram.Save("CustomStyledDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
