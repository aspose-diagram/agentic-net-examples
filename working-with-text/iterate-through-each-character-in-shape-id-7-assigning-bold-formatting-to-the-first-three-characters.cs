using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace the path with your file)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page
            Page page = diagram.Pages[0];

            // Retrieve the shape with ID 7
            Shape shape = page.Shapes.GetShape(7L);
            if (shape == null)
            {
                Console.WriteLine("Shape with ID 7 not found.");
                return;
            }

            // Apply bold formatting to the first three characters of the shape's text
            for (int i = 0; i < 3; i++)
            {
                Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                ch.IX = i; // character index
                ch.Style.Value = StyleValue.Bold; // set bold style
                shape.Chars.Add(ch);
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Bold formatting applied to the first three characters of shape ID 7.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
