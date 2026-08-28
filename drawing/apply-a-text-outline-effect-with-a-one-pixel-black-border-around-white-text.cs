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
            Diagram diagram = new Diagram();

            // Use the first page (created by default)
            Page page = diagram.Pages[0];

            // Add a rectangle shape that will contain the text
            double pinX = 5.0;      // X position (in inches)
            double pinY = 5.0;      // Y position (in inches)
            double width = 3.0;     // Width of the rectangle (in inches)
            double height = 1.0;    // Height of the rectangle (in inches)

            long shapeId = page.AddShape(pinX, pinY, width, height, "Rectangle");
            Shape shape = page.Shapes.GetShape(shapeId);

            // Clear any existing text and add the desired text
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Outlined Text"));

            // Create a character formatting run for the text
            Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
            ch.IX = 0;                     // Start index of the run
            ch.Color.Value = "#FFFFFF";    // White fill color for the text

            // Aspose.Diagram does not expose a direct text‑outline property.
            // As a fallback, we set the shape's line color to black, which gives a
            // one‑pixel black border around the shape (not the text itself).
            shape.Line.LineColor.Value = "#000000";

            // Apply the character formatting to the shape
            shape.Chars.Add(ch);

            // Save the diagram
            diagram.Save("OutlinedText.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
