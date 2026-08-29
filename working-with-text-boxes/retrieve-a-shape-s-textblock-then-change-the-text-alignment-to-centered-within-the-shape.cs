using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Ensure there is at least one shape on the page
            if (page.Shapes.Count == 0)
            {
                Console.WriteLine("No shapes found in the diagram.");
                return;
            }

            // Retrieve the first shape on the page
            Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);

            // Center the text vertically within the shape
            shape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;

            // Center the text horizontally within the shape (first paragraph)
            if (shape.Paras.Count > 0)
            {
                shape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Text alignment updated and diagram saved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
