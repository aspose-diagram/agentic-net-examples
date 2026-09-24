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

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Get the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Find a shape that contains at least two paragraphs
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Paras.Count > 1)
                {
                    // Apply italic style and dark blue color to the characters of this shape
                    foreach (Aspose.Diagram.Char ch in shape.Chars)
                    {
                        // Preserve existing styles and add Italic
                        ch.Style.Value |= StyleValue.Italic;
                        // Dark blue color (hex code)
                        ch.Color.Value = "#00008B";
                    }

                    // If only the first matching shape should be modified, exit the loop
                    break;
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
