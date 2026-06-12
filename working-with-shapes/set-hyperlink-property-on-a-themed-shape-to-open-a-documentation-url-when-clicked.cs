using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Add a rectangle shape to the active page at position (2,2)
                long shapeId = diagram.ActivePage.AddShape(2.0, 2.0, "Rectangle");

                // Retrieve the shape instance using the returned ID
                Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);

                // Apply a preset theme to the shape (theme is write‑only)
                shape.PresetTheme = PresetThemeValue.Bubble;
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                // Create a hyperlink that points to the documentation URL
                Hyperlink link = new Hyperlink
                {
                    Name = "DocumentationLink"
                };
                link.Address.Value = "https://example.com/documentation";

                // Add the hyperlink to the shape's Hyperlinks collection
                shape.Hyperlinks.Add(link);

                // Save the diagram to a VSDX file
                diagram.Save("ThemedShapeWithLink.vsdx", SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram saved with a themed shape containing a hyperlink.");

            }
            catch (System.NullReferenceException ex)
            {
                Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
            }
    }
    }