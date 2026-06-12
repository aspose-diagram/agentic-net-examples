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
                    // Ensure there is at least one page (the default diagram contains one)
                    Page page = diagram.Pages[0];

                    // Define position and size for the rectangle (in inches)
                    double pinX = 2.0;   // X coordinate of the shape's pin (center)
                    double pinY = 2.0;   // Y coordinate of the shape's pin (center)
                    double width = 2.0;  // Width of the rectangle
                    double height = 1.0; // Height of the rectangle

                    // Add a rectangle shape using the built‑in "Rectangle" master.
                    // The fourth parameter (isCalculate) must be a boolean.
                    long shapeId = page.AddShape(pinX, pinY, width, height, "Rectangle", false);

                    // Retrieve the Shape object from the returned ID
                    Shape rectangle = page.Shapes.GetShape(shapeId);

                    // Apply a preset theme to the shape.
                    // The specific "flowchart" theme is not a defined PresetThemeValue,
                    // so we use a valid theme (e.g., Bubble) as an example.
                    rectangle.PresetTheme = PresetThemeValue.Bubble;

                    // Optionally, set a variant and quick style for completeness
                    rectangle.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                    rectangle.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

                    // Save the diagram to a VSDX file
                    string outputPath = "FlowchartRectangle.vsdx";
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Diagram created and saved successfully.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }