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

                // Add a new page to the diagram (required before adding shapes)
                diagram.Pages.Add(new Page());

                // Reference the first (and only) page
                Page page = diagram.Pages[0];

                // Insert a rectangle shape at position (2, 2) inches
                // The AddShape method returns the shape's unique ID (long)
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

                // Retrieve the shape instance using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Apply a solid fill pattern
                shape.Fill.FillPattern.Value = 1; // 1 = solid

                // Set the foreground fill color to teal using a hex RGB value
                // Teal RGB = (0, 128, 128) => hex "#008080"
                shape.Fill.FillForegnd.Value = "#008080";

                // Save the diagram to a VSDX file
                diagram.Save("TealShapeDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }