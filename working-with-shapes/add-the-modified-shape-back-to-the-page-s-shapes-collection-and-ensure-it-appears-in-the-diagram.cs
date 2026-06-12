using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page in the diagram
            Page page = diagram.Pages[0];

            // Retrieve a shape to modify (example: the first shape on the page)
            Shape originalShape = page.Shapes[0];
            long shapeId = originalShape.ID;

            // Get the shape instance by its ID (ensures we have the latest reference)
            Shape shape = page.Shapes.GetShape(shapeId);

            // Example modification: change the fill foreground color to red
            shape.Fill.FillForegnd.Value = "#FF0000";

            // Ensure the modified shape is present in the page's Shapes collection
            // (avoid duplicate addition if it already exists)
            bool alreadyExists = false;
            foreach (Shape s in page.Shapes)
            {
                if (s.ID == shape.ID)
                {
                    alreadyExists = true;
                    break;
                }
            }

            if (!alreadyExists)
            {
                page.Shapes.Add(shape);
            }

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
