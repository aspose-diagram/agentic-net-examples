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

            // Load an existing Visio file; if it does not exist, create a new diagram with a blank page.
            string inputPath = "input.vsdx";
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception)
            {
                diagram = new Diagram();
                diagram.Pages.Add(new Page());
            }

            // Access the first page of the diagram.
            Page page = diagram.Pages[0];

            // Add a new rectangle shape to the page.
            double pinX = 2.0; // inches
            double pinY = 2.0; // inches
            string masterName = "Rectangle";
            long shapeId = page.AddShape(pinX, pinY, masterName);

            // Retrieve the shape instance using the returned ID.
            Shape shape = page.Shapes.GetShape(shapeId);

            // Modify the shape: set its text and fill colour.
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Hello Aspose"));
            shape.Fill.FillForegnd.Value = "#FFCC00"; // orange fill

            // The shape is already part of the page's Shapes collection.
            // Save the diagram to a new file.
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
