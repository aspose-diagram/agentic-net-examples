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

                // Get the first (default) page
                Page page = diagram.Pages[0];

                // Add sample shapes using the built‑in "Rectangle" master
                long rectId1 = page.AddShape(2.0, 2.0, "Rectangle");
                long rectId2 = page.AddShape(5.0, 2.0, "Rectangle");
                long rectId3 = page.AddShape(8.0, 2.0, "Rectangle");

                // Retrieve the shape objects
                Shape shape1 = page.Shapes.GetShape(rectId1);
                Shape shape2 = page.Shapes.GetShape(rectId2);
                Shape shape3 = page.Shapes.GetShape(rectId3);

                // For demonstration, explicitly set a custom line color on shape2
                // This disables line inheritance for that shape
                shape2.Line.LineColor.Value = "#0000FF"; // Blue line

                // Iterate all shapes on the page
                foreach (Shape shp in page.Shapes)
                {
                    // Determine if line inheritance is disabled:
                    // If the shape's line color differs from the inherited line color,
                    // inheritance is considered disabled.
                    bool lineInheritanceDisabled = shp.Line.LineColor.Value != shp.InheritLine.LineColor.Value;

                    if (lineInheritanceDisabled)
                    {
                        // Highlight the shape with a custom color (e.g., bright red)
                        shp.Line.LineColor.Value = "#FF0000";
                    }
                    else
                    {
                        // Optionally, set a different color for shapes that inherit lines
                        shp.Line.LineColor.Value = "#00FF00"; // Green line
                    }
                }

                // Save the diagram to a VSDX file
                string outputPath = "HighlightedInheritance.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }