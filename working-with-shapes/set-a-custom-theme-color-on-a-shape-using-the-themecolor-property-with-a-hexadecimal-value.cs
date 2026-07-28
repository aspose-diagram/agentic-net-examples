using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Use the active page to add a shape
            Page page = diagram.ActivePage;

            // Add a rectangle shape at position (2,2) – returns the shape ID
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

            // Retrieve the shape instance by its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Set a custom fill color using a hexadecimal value (ThemeColor property does not exist)
            shape.Fill.FillForegnd.Value = "#FF5733";

            // Save the diagram to a VSDX file
            diagram.Save("CustomThemeColor.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}