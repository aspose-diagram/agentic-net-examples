using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input and output diagrams
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Process only connector shapes (1‑D objects)
                    if (shape.OneD)
                    {
                        // If the connector uses the default/straight jump style, change it to Square
                        if (shape.Layout.ConLineJumpStyle.Value == ConLineJumpStyleValue.PageDefault ||
                            shape.Layout.ConLineJumpStyle.Value == ConLineJumpStyleValue.Undefined)
                        {
                            shape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.Square;
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
