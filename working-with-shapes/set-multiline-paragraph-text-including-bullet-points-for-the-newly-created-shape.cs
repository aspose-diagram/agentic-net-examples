using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Use the first page (a new diagram contains one default page)
        Page page = diagram.Pages[0];

        // Draw a simple rectangle shape (returns the shape ID)
        long shapeId = page.DrawRectangle(2.0, 2.0, 4.0, 3.0);

        // Retrieve the shape instance from its ID
        Shape shape = page.Shapes.GetShape(shapeId);

        // Clear any existing text
        shape.Text.Value.Clear();

        // Add three separate text runs – each will become a paragraph
        shape.Text.Value.Add(new Txt("First bullet point"));
        shape.Text.Value.Add(new Txt("\nSecond bullet point"));
        shape.Text.Value.Add(new Txt("\nThird bullet point"));

        // Ensure there are three paragraphs (one per line)
        // Apply bullet formatting and indentation to each paragraph
        for (int i = 0; i < shape.Paras.Count && i < 3; i++)
        {
            // Set bullet style (standard solid bullet)
            shape.Paras[i].Bullet.Value = BulletValue.Style1;

            // Optional: set left indentation (in inches)
            shape.Paras[i].IndLeft.Value = 0.2;   // 0.2 inches from the left margin
            shape.Paras[i].IndFirst.Value = 0.1; // first line indent

            // Optional: set paragraph spacing
            shape.Paras[i].SpBefore.Value = 0.05; // space before paragraph
            shape.Paras[i].SpAfter.Value = 0.05;  // space after paragraph
        }

        // Save the diagram to a VSDX file
        diagram.Save("MultilineBulletShape.vsdx", SaveFileFormat.Vsdx);
    }
}
