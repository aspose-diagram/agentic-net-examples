using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Add a new page (the diagram already contains a default page)
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page
                // PinX and PinY are the center coordinates (in inches)
                double pinX = 5.0;
                double pinY = 5.0;
                long shapeId = page.AddShape(pinX, pinY, "Rectangle");

                // Retrieve the shape object using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Clear any existing text and add new text
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt("Sample Text"));

                // Create a character formatting entry for the text run
                Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                ch.IX = 0;                         // Index of the character run
                ch.Color.Value = "#FFFFFF";       // White fill color for the text
                shape.Chars.Add(ch);

                // Apply a thin black border around the text by using the shape's line formatting
                // This simulates a text outline effect (1 pixel ≈ 0.01 inches)
                shape.Line.LineColor.Value = "#000000";   // Black border color
                shape.Line.LineWeight.Value = 0.01;       // Line weight (in inches)

                // Save the diagram to a VSDX file
                diagram.Save("TextOutlineDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }