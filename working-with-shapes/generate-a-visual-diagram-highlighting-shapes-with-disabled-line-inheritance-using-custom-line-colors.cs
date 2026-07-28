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

                // Add a rectangle shape (inherits line formatting from its master)
                long rectId = diagram.AddShape(2.0, 2.0, "Rectangle", 0);
                Shape rectShape = page.Shapes.GetShape(rectId);

                // Add another rectangle shape and explicitly set its line color
                // This disables line inheritance for this shape
                long customRectId = diagram.AddShape(5.0, 2.0, "Rectangle", 0);
                Shape customRectShape = page.Shapes.GetShape(customRectId);
                // Disable inheritance by assigning a different line color
                customRectShape.Line.LineColor.Value = "#00FF00"; // green line

                // Iterate all shapes on the page and highlight those with disabled line inheritance
                foreach (Shape shape in page.Shapes)
                {
                    // Compare the shape's line color with its inherited line color
                    // If they differ, inheritance is disabled
                    if (shape.Line.LineColor.Value != shape.InheritLine.LineColor.Value)
                    {
                        // Apply a custom highlight color (red) to the shape's line
                        shape.Line.LineColor.Value = "#FF0000";
                        // Optionally, make the line thicker for visibility
                        shape.Line.LineWeight.Value = 0.05; // inches
                    }
                }

                // Save the diagram to a VSDX file
                string outputPath = "HighlightedDiagram.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved to {outputPath}");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }