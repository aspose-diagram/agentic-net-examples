using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page
            // Parameters: pinX, pinY, width, height, master name, isCalculate
            long shapeId = page.AddShape(2.0, 2.0, 2.0, 2.0, "Rectangle", false);
            Shape shape = page.Shapes.GetShape(shapeId);

            // -------------------------------------------------
            // Insert a new geometry vertex at a defined coordinate
            // (e.g., X = 1.0, Y = 1.0) into the first geometry section
            // -------------------------------------------------
            // Ensure the shape has at least one geometry section
            if (shape.Geoms.Count > 0)
            {
                // Create a new LineTo segment (vertex)
                LineTo vertex = new LineTo();
                vertex.X.Value = 1.0; // X coordinate in inches
                vertex.Y.Value = 1.0; // Y coordinate in inches

                // Append the vertex to the coordinate collection
                shape.Geoms[0].CoordinateCol.Add(vertex);
            }

            // -------------------------------------------------
            // Add a text field to the shape to display dynamic text
            // -------------------------------------------------
            Field field = new Field();
            // Set the displayed value of the field
            field.Value.Val = "Dynamic Text";
            // Add the field to the shape's field collection
            shape.Fields.Add(field);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
