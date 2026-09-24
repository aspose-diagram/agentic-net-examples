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

            // Add a new page to the diagram
            Page page = new Page();
            diagram.Pages.Add(page);

            // Draw a simple rectangle shape on the page (pinX, pinY, width, height)
            long shapeId = page.DrawRectangle(2.0, 2.0, 4.0, 4.0);

            // Retrieve the shape object using its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Override inherited fill colors by setting the shape's Fill properties directly
            shape.Fill.FillBkgnd.Value = "#ADD8E6";   // Light blue background
            shape.Fill.FillForegnd.Value = "#FF0000"; // Red foreground

            // Save the diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}