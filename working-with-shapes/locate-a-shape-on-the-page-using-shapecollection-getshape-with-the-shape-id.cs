using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be loaded
            string inputPath = "input.vsdx";

            // Load the Visio diagram (uses the provided load rule)
            Diagram diagram = new Diagram(inputPath);

            // Specify the shape ID you want to locate
            long shapeId = 5; // replace with the actual ID

            // Access the first page (or any specific page)
            Page page = diagram.Pages[0];

            // Locate the shape on the page using GetShape with the shape ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Example usage: display some properties of the found shape
            Console.WriteLine($"Found Shape - ID: {shape.ID}, Name: {shape.Name}");

            // Save the diagram if any modifications were made (uses the provided save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
