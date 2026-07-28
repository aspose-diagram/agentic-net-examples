using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram using the provided load rule
            Diagram diagram = LoadDiagram("input.vsdx");

            // Identifier of the shape to resize (example value)
            long shapeId = 1;

            // Desired dimensions in inches
            double newWidth = 2.5;
            double newHeight = 1.5;

            // Retrieve the shape from the first page
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Resize the shape
            shape.SetWidth(newWidth);
            shape.SetHeight(newHeight);

            // Save the diagram using the provided save rule
            SaveDiagram(diagram, "output.vsdx");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Placeholder for the provided load rule
    static Diagram LoadDiagram(string filePath)
    {
        // The actual implementation is supplied by the lifecycle rule
        return new Diagram(filePath);
    }

    // Placeholder for the provided save rule
    static void SaveDiagram(Diagram diagram, string filePath)
    {
        // The actual implementation is supplied by the lifecycle rule
        diagram.Save(filePath, SaveFileFormat.Vsdx);
    }
}
