using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new diagram
            using (Diagram diagram = new Diagram())
            {
                // Access the first page (default page)
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page
                // Parameters: pinX, pinY, master name, isCalculate (bool)
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);

                // Retrieve the shape instance
                Shape shape = page.Shapes.GetShape(shapeId);

                // Add a text block (text run) to the shape
                shape.Text.Value.Add(new Txt("Sample Text"));

                // Create a character formatting entry for the text
                Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                ch.IX = 0; // Index of the character run
                ch.FontName.Value = "Arial";               // Set font name
                ch.Size.Value = 12.0 / 72.0;                // Font size in inches (12 points)

                // Apply the character formatting to the shape
                shape.Chars.Add(ch);

                // Save the diagram to a VSDX file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
