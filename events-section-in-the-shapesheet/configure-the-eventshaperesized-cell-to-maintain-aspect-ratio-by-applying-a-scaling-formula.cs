using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page
            Page page = diagram.Pages[0];

            // Add a rectangle shape
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle");
            Shape shape = page.Shapes.GetShape(shapeId);

            // Apply a scaling formula to maintain aspect ratio on a supported event cell
            shape.Event.EventXFMod.Ufe.F = "GUARD(Width * 0.5)";

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            diagram.Dispose();

            Console.WriteLine("Diagram processed and saved successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}