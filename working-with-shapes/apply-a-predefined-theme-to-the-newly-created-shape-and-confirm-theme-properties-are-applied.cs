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

                // Access the first page (automatically created)
                Page page = diagram.Pages[0];

                // Add a rectangle shape at position (2,2) inches
                // Using the master name "Rectangle" which exists in the default stencil
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

                // Retrieve the concrete Shape object
                Shape shape = page.Shapes.GetShape((int)shapeId);

                // Apply a predefined theme to the shape
                // These properties are write‑only; we set them and log the action
                shape.PresetTheme = PresetThemeValue.Bubble;
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

                Console.WriteLine("Theme applied to shape ID {0}: PresetTheme=Bubble, Variant=Variant1, QuickStyle=VariantStyle1", shapeId);

                // Save the diagram to verify the theme is persisted
                string outputPath = "ThemedDiagram.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved to: " + outputPath);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }