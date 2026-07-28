using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new diagram
                Diagram diagram = new Diagram();

                // Get the active page where the shape will be added
                Page page = diagram.ActivePage;

                // Add a rectangle shape at position (2,2) inches
                // This overload returns the shape ID as a long
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

                // Retrieve the concrete Shape object using the ID
                Shape shape = page.Shapes.GetShape((int)shapeId);

                // Apply a preset theme to the shape (optional, demonstrates a themed shape)
                shape.PresetTheme = PresetThemeValue.Bubble;
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                // Create a new hyperlink instance
                Hyperlink link = new Hyperlink();
                // Set the URL that will be opened when the shape is clicked
                link.Address.Value = "https://example.com/documentation";
                // Optional: set a description (tooltip) for the hyperlink
                link.Description.Value = "Open documentation";

                // Add the hyperlink to the shape's Hyperlinks collection
                shape.Hyperlinks.Add(link);

                // Save the diagram to a VSDX file
                diagram.Save("ThemedShapeWithLink.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.NullReferenceException ex)
            {
                Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
            }
    }
    }