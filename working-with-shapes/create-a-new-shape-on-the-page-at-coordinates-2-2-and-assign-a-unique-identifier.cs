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
            using (Diagram diagram = new Diagram())
            {
                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    diagram.Pages.Add(new Page());
                }

                // Get the first page
                Page page = diagram.Pages[0];

                // Add a rectangle shape at coordinates (2,2)
                // The fourth parameter 'isCalculate' must be a boolean
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);

                // Retrieve the shape object using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Assign a unique identifier (using current ticks for uniqueness)
                shape.ID = DateTime.Now.Ticks;

                // Save the diagram to a VSDX file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
