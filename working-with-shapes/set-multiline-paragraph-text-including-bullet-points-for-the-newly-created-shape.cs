using System.IO;
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

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Add a rectangle shape at position (2,2)
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

            // Retrieve the shape object using its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Clear any existing text and paragraph collections
            shape.Text.Value.Clear();
            shape.Paras.Clear();

            // Paragraph 1 – normal text
            Para para1 = new Para();
            shape.Paras.Add(para1);
            shape.Text.Value.Add(new Txt("First line"));

            // Paragraph 2 – normal text
            Para para2 = new Para();
            shape.Paras.Add(para2);
            shape.Text.Value.Add(new Txt("Second line"));

            // Paragraph 3 – bullet point
            Para para3 = new Para();
            para3.Bullet.Value = BulletValue.Style1;
            shape.Paras.Add(para3);
            shape.Text.Value.Add(new Txt("Bullet point 1"));

            // Paragraph 4 – bullet point
            Para para4 = new Para();
            para4.Bullet.Value = BulletValue.Style1;
            shape.Paras.Add(para4);
            shape.Text.Value.Add(new Txt("Bullet point 2"));

            // Save the diagram to a VSDX file
            diagram.Save("MultilineBulletShape.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
