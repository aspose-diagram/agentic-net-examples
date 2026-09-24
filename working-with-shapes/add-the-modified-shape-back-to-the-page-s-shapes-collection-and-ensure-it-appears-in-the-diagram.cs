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

            // Load an existing diagram or create a new empty one
            Diagram diagram = new Diagram();

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                diagram.Pages.Add(new Page());
            }

            // Get the first page
            Page page = diagram.Pages[0];

            // Add a new rectangle shape to the page
            // Parameters: pinX, pinY, width, height, master name, isCalculate
            long shapeId = page.AddShape(2.0, 2.0, 2.0, 2.0, "Rectangle", false);

            // Retrieve the shape object using the returned ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Modify the shape: clear existing text, add new text, and set fill color
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Hello Aspose"));
            shape.Fill.FillForegnd.Value = "#FFCC00";

            // If the shape had been removed from the collection, it could be re‑added:
            // page.Shapes.Add(shape);

            // Save the diagram to verify the shape appears
            diagram.Save("ModifiedDiagram.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved with the modified shape.");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
