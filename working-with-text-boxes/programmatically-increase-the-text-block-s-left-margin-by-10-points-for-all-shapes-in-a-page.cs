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

            // Choose the page to modify (e.g., the first page)
            Page page = diagram.Pages[0];

            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Ensure the shape has a TextBlock (some shapes may not contain text)
                if (shape.TextBlock != null && shape.TextBlock.LeftMargin != null)
                {
                    // Increase the left margin by 10 points
                    shape.TextBlock.LeftMargin.Value += 10;
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
