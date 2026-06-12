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

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page
            Shape shape = null;
            foreach (Shape s in page.Shapes)
            {
                shape = s;
                break;
            }

            if (shape == null)
            {
                throw new Exception("No shape found on the page.");
            }

            // Ensure the shape has at least one paragraph for horizontal alignment
            if (shape.Paras.Count == 0)
            {
                shape.Paras.Add(new Para());
            }

            // Center the text horizontally within the shape
            shape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;

            // Center the text vertically within the shape using TextBlock
            shape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
