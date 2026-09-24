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

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Get the first page (or any specific page you need)
            Page page = diagram.Pages[0];

            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Ensure the shape has a TextBlock before modifying
                if (shape.TextBlock != null)
                {
                    // Increase the left margin by 10 points
                    shape.TextBlock.LeftMargin.Value += 10;
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
