using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Loop through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Loop through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape has a TextBlock (all shapes do, but guard against null)
                    if (shape.TextBlock != null)
                    {
                        // Increase the left margin by 10 points
                        shape.TextBlock.LeftMargin.Value += 10;
                    }
                }
            }

            // Save the updated diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
