using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for SaveFileFormat enum

namespace DiagramTextBlockExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Wrap all Aspose operations in a try/catch to capture runtime errors
            try
            {
                // Create a new empty diagram inside a using block to ensure disposal
                using (Diagram diagram = new Diagram())
                {
                    // Access the first (default) page of the diagram
                    Page page = diagram.Pages[0];

                    // Add a rectangle shape to the page
                    // AddShape returns a long shape ID; retrieve the Shape object via Shapes.GetShape
                    long shapeId = page.AddShape(2.0, 2.0, 2.0, 1.0, "Rectangle", false);
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Add a text block (plain text) to the shape
                    shape.Text.Value.Add(new Txt("Sample Text"));

                    // Create a character formatting entry for the text
                    Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                    ch.IX = 0; // Index of the first character run
                    ch.FontName.Value = "Arial";               // Set font name to Arial
                    ch.Size.Value = 12.0 / 72.0;                // Set font size to 12 points (in inches)

                    // Apply the character formatting to the shape
                    shape.Chars.Add(ch);

                    // Save the diagram to a VSDX file using the correct SaveFileFormat enum member
                    diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Diagram created with text block using Arial 12pt.");
            }
            catch (Exception ex)
            {
                // Write any errors to the error stream
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}