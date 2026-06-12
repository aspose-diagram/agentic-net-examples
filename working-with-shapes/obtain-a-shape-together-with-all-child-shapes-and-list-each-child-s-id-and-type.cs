using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram using the prescribed load rule
            Diagram diagram = LoadDiagram("input.vsdx");

            // Identify the parent shape (replace with the actual ID or name as needed)
            int parentShapeId = 1; // example ID
            Shape parentShape = diagram.Pages[0].Shapes.GetShapeIncludingChild(parentShapeId);

            // Iterate through all child shapes of the parent and output their ID and Type
            foreach (Shape child in parentShape.Shapes)
            {
                Console.WriteLine($"Child ID: {child.ID}, Type: {child.Type}");
            }

            // Save the diagram using the prescribed save rule
            SaveDiagram(diagram, "output.vsdx");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Load rule placeholder – actual implementation is provided by the lifecycle rule set
    static Diagram LoadDiagram(string filePath)
    {
        return new Diagram(filePath);
    }

    // Save rule placeholder – actual implementation is provided by the lifecycle rule set
    static void SaveDiagram(Diagram diagram, string filePath)
    {
        diagram.Save(filePath, SaveFileFormat.Vsdx);
    }
}
