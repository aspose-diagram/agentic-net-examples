using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new blank diagram
                Diagram diagram = new Diagram();

                // Get the first (default) page
                Page page = diagram.Pages[0];

                // Add a rectangle shape at position (2,2) inches
                // The AddShape overload returns the shape ID (long)
                long rectShapeId = page.AddShape(2.0, 2.0, "Rectangle");

                // Retrieve the shape object using the returned ID
                Shape rectShape = page.Shapes.GetShape(rectShapeId);

                // Apply a preset theme.
                // The Visio "Flowchart" theme is not exposed as a PresetThemeValue enum member,
                // so we use an available theme (e.g., Bubble) as an example.
                // Replace with the appropriate enum value if a Flowchart theme becomes available.
                page.PresetTheme = PresetThemeValue.Bubble;

                // Save the diagram to a VSDX file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }