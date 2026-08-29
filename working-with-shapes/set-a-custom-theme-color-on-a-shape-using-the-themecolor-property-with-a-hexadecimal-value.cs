using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for SaveFileFormat

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the active page of the diagram
            Page page = diagram.ActivePage;

            // Add a rectangle shape at coordinates (2, 2)
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

            // Retrieve the shape instance using its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Apply a custom fill color (used as a theme color substitute) using a hexadecimal string
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