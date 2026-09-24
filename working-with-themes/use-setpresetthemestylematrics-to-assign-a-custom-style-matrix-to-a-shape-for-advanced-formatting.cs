using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new blank diagram
                Diagram diagram = new Diagram();

                // Access the first (default) page
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page
                // Parameters: pinX, pinY, master name, isCalculate (bool)
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);

                // Retrieve the Shape object using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Apply a preset theme and variant to the shape
                shape.PresetTheme = PresetThemeValue.Bubble;
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                // Assign a custom style matrix (style and color) for advanced formatting
                shape.SetPresetThemeStyleMatrics(PresetStyleMatricsValue.Style2, PresetColorMatricsValue.Color7);

                // Optionally add some text to the shape
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt("Themed Shape"));

                // Save the diagram to a VSDX file
                string outputPath = "ThemedDiagram.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }