using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first page (a new diagram contains one default page)
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page (pinX, pinY, width, height)
            long shapeId = page.DrawRectangle(2.0, 2.0, 4.0, 2.0);

            // Retrieve the shape instance
            Shape shape = page.Shapes.GetShape(shapeId);

            // Clear any existing text
            shape.Text.Value.Clear();

            // ----- First paragraph (bullet) -----
            // Create a paragraph and set bullet style
            Aspose.Diagram.Para para1 = new Aspose.Diagram.Para();
            para1.Bullet.Value = BulletValue.Style1; // standard bullet
            // Add the paragraph to the shape
            shape.Paras.Add(para1);
            // Add the text run for this paragraph
            shape.Text.Value.Add(new Txt("First bullet point"));

            // ----- Second paragraph (bullet) -----
            Aspose.Diagram.Para para2 = new Aspose.Diagram.Para();
            para2.Bullet.Value = BulletValue.Style1;
            shape.Paras.Add(para2);
            shape.Text.Value.Add(new Txt("Second bullet point"));

            // ----- Third paragraph (bullet) -----
            Aspose.Diagram.Para para3 = new Aspose.Diagram.Para();
            para3.Bullet.Value = BulletValue.Style1;
            shape.Paras.Add(para3);
            shape.Text.Value.Add(new Txt("Third bullet point"));

            // Save the diagram to VSDX format
            diagram.Save("MultilineBulletShape.vsdx", SaveFileFormat.Vsdx);
        }
    }