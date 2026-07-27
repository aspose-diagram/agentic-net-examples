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

            // Get the first page
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID (example uses ID = 1)
            // Adjust the ID as needed for your diagram
            Shape shape = page.Shapes.GetShape(1);

            // Clear any existing text
            shape.Text.Value.Clear();

            // Add new text to the shape
            shape.Text.Value.Add(new Txt("Sample Text"));

            // Create a character formatting entry for the text
            Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
            ch.IX = 0; // Index of the character run
            ch.FontName.Value = "Arial";               // Set font name
            ch.Size.Value = 12.0 / 72.0;                // Set font size (points to inches)

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
