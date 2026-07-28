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

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Add a new rectangle shape to the diagram (master name: "Rectangle")
            // The AddShape method returns the shape's unique ID (type long)
            long shapeId = diagram.AddShape(2.0, 2.0, "Rectangle", 0);

            // Retrieve the newly created shape from the page's Shapes collection
            Shape shape = page.Shapes.GetShape(shapeId);

            // Modify the shape's properties (e.g., text and formatting)
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Hello Aspose"));
            shape.Fill.FillForegnd.Value = "#FFCC00";   // Fill color
            shape.Line.LineColor.Value = "#000000";    // Line color

            // The shape is already part of the page's Shapes collection because
            // it was added via diagram.AddShape. No further action is required.

            // Save the diagram to verify that the modified shape appears
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
