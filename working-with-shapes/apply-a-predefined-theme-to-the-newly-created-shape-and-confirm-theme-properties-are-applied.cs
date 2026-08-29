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
            using (Diagram diagram = new Diagram())
            {
                // Add a rectangle shape to the active page
                // Parameters: PinX, PinY, master name
                long shapeId = diagram.ActivePage.AddShape(2.0, 2.0, "Rectangle");

                // Retrieve the concrete Shape object using the returned ID
                Shape shape = diagram.ActivePage.Shapes.GetShape((int)shapeId);

                // Apply a predefined theme to the newly created shape
                shape.PresetTheme = PresetThemeValue.Bubble;
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

                // Since theme properties are write‑only, confirm by logging the actions
                Console.WriteLine($"Shape ID {shape.ID} created and theme applied:");
                Console.WriteLine($"  PresetTheme = Bubble");
                Console.WriteLine($"  PresetThemeVariant = Variant1");
                Console.WriteLine($"  PresetThemeQuickStyle = VariantStyle1");

                // Save the diagram to a VSDX file to persist the theme
                string outputPath = "ThemedDiagram.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");
            }

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
