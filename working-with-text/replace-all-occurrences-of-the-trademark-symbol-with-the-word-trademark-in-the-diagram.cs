using System.IO;
using System;
using Aspose.Diagram;

class ReplaceTrademarkSymbol
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram from a file
            var diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Replace every occurrence of the trademark symbol (™) with the word "Trademark"
                    shape.ReplaceText("™", "Trademark");
                }
            }

            // Save the modified diagram to a new file (preserving the original format)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
