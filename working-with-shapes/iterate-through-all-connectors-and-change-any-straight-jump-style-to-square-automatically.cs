using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify connector shapes (1‑D objects)
                    if (shape.OneD)
                    {
                        // Retrieve current line jump style
                        var currentJump = shape.Layout.ConLineJumpStyle.Value;

                        // If the jump style is the default straight style, change it to square
                        if (currentJump == ConLineJumpStyleValue.PageDefault)
                        {
                            shape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.Square;
                        }
                    }
                }
            }

            // Save the updated diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
