using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID (example uses ID = 1)
            Shape shape = page.Shapes.GetShape(1);

            // Clear any existing text in the shape
            shape.Text.Value.Clear();

            // Add a new text block to the shape
            shape.Text.Value.Add(new Txt("Sample Text"));

            // Create character formatting for the added text
            Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
            ch.IX = 0; // Index of the first character run
            ch.FontName.Value = "Arial";               // Set font name to Arial
            ch.Size.Value = 12.0 / 72.0;                // Set font size to 12 points (converted to inches)

            // Apply the character formatting to the shape
            shape.Chars.Add(ch);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
