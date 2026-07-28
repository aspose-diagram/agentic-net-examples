using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty Visio diagram
            Diagram diagram = new Diagram();

            // Retrieve the first (default) page
            Page page = diagram.Pages[0];

            // Parameters for a rectangle shape
            double pinX = 5.0;      // X coordinate (in inches)
            double pinY = 5.0;      // Y coordinate (in inches)
            double width = 2.0;     // Width (in inches)
            double height = 1.0;    // Height (in inches)
            string masterName = "Rectangle";

            // Add the rectangle shape to the diagram on the current page
            long shapeIdLong = diagram.AddShape(pinX, pinY, width, height, masterName, page.ID);
            // Convert the long ID to int for GetShape
            Shape shape = page.Shapes.GetShape((int)shapeIdLong);

            // ----- Create user‑defined cells (custom properties) -----
            // Custom cell 1: CustomWidth
            User customWidth = new User();
            customWidth.Name = "CustomWidth";
            customWidth.Value.Val = "800";                     // Value stored as string
            customWidth.Prompt.Value = "Custom width in pixels";

            // Custom cell 2: CustomDescription
            User customDesc = new User();
            customDesc.Name = "CustomDescription";
            customDesc.Value.Val = "Template shape for reuse";
            customDesc.Prompt.Value = "Description of the shape";

            // Add the custom cells to the shape's Users collection
            shape.Users.Add(customWidth);
            shape.Users.Add(customDesc);

            // Optional: set visible text for the shape
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Template Shape"));

            // Save the diagram as a VSDX template file
            diagram.Save("TemplateDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
