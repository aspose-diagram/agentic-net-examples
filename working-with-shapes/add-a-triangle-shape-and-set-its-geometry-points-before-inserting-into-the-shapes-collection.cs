using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "TriangleDiagram.vsdx";

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the active page where the shape will be added
            Page page = diagram.ActivePage;

            // Add a placeholder rectangle shape (master name "Rectangle") at position (2,2)
            // The fourth parameter 'isCalculate' is set to false as we will customize geometry manually
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);

            // Retrieve the newly added shape using its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // ------------------------------------------------------------
            // Set custom geometry to turn the rectangle into a triangle
            // ------------------------------------------------------------
            // NOTE: The exact API for editing geometry rows may vary between versions.
            // The following steps outline the intended actions:
            // 1. Clear existing geometry rows.
            // 2. Add new geometry rows that define the three vertices of a triangle.
            // 3. Set the appropriate formulas for each vertex (e.g., MoveTo, LineTo).
            // ------------------------------------------------------------
            // TODO: Implement geometry editing using the appropriate Aspose.Diagram APIs.
            // Example (pseudo-code):
            // shape.Geometry.Row.Clear();
            // Row moveTo = new Row();
            // moveTo.Type = GeometryRowType.MoveTo;
            // moveTo.X.Value = "Width*0.5";
            // moveTo.Y.Value = "Height";
            // shape.Geometry.Row.Add(moveTo);
            // Row line1 = new Row();
            // line1.Type = GeometryRowType.LineTo;
            // line1.X.Value = "0";
            // line1.Y.Value = "0";
            // shape.Geometry.Row.Add(line1);
            // Row line2 = new Row();
            // line2.Type = GeometryRowType.LineTo;
            // line2.X.Value = "Width";
            // line2.Y.Value = "0";
            // shape.Geometry.Row.Add(line2);
            // Row close = new Row();
            // close.Type = GeometryRowType.Close;
            // shape.Geometry.Row.Add(close);
            // ------------------------------------------------------------

            // Save the diagram to a VSDX file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}