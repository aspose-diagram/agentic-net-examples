using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Prompt the user for the desired line thickness (in inches)
        Console.Write("Enter line thickness (in inches): ");
        string input = Console.ReadLine();

        // Validate the input
        if (!double.TryParse(input, out double thickness) || thickness <= 0)
        {
            Console.WriteLine("Invalid thickness value.");
            return;
        }

        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Add a new page to the diagram
        diagram.Pages.Add(new Page());

        // Get the first (and only) page
        Page page = diagram.Pages[0];

        // Draw a circle using DrawEllipse (width == height)
        // Center at (5,5) with a diameter of 2 inches
        long shapeId = page.DrawEllipse(5.0, 5.0, 2.0, 2.0);

        // Retrieve the shape object by its ID
        Shape circle = page.Shapes.GetShape(shapeId);

        // Set the line thickness (weight) in inches
        circle.Line.LineWeight.Value = thickness;

        // Save the diagram to a VSDX file
        diagram.Save("Circle.vsdx", SaveFileFormat.Vsdx);

        Console.WriteLine($"Diagram saved as Circle.vsdx with line thickness {thickness} inches.");
    }
}
