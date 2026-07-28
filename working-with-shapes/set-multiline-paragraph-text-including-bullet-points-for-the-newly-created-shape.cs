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

            // Get the first page (active page)
            Page page = diagram.ActivePage;

            // Add a rectangle shape to the page
            // Parameters: PinX, PinY, master name
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

            // Retrieve the shape object using the returned ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Clear any existing text
            shape.Text.Value.Clear();

            // Add text runs (each line ends with a newline character)
            shape.Text.Value.Add(new Txt("First bullet point"));
            shape.Text.Value.Add(new Txt("\n"));
            shape.Text.Value.Add(new Txt("Second bullet point"));
            shape.Text.Value.Add(new Txt("\n"));
            shape.Text.Value.Add(new Txt("A normal paragraph without bullet"));

            // Ensure there are enough paragraphs (one per line)
            // The first paragraph already exists; add two more
            while (shape.Paras.Count < 3)
            {
                shape.Paras.Add(new Para());
            }

            // Set bullet style for the first two paragraphs
            shape.Paras[0].Bullet.Value = BulletValue.Style1;      // First bullet
            shape.Paras[1].Bullet.Value = BulletValue.Style1;      // Second bullet
            // No bullet for the third paragraph (default)

            // Save the diagram to a VSDX file
            diagram.Save("MultilineBulletShape.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
