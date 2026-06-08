using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first (default) page
            Page page = diagram.Pages[0];

            // Draw a rectangle shape on the page
            // Parameters: pinX, pinY, width, height (all in inches)
            long shapeId = page.DrawRectangle(2.0, 2.0, 1.0, 1.0);

            // Retrieve the Shape object using the returned ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Clear any existing text and assign a static string
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Custom Text"));

            // Save the diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
    }