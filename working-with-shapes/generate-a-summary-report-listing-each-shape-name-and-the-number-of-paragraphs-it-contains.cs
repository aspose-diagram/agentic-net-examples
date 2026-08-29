using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Get the shape name; if Name is null, use an empty string
                    string shapeName = shape.Name ?? string.Empty;

                    // Count the number of paragraphs (Paras) in the shape
                    int paragraphCount = shape.Paras.Count;

                    // Output the summary line for this shape
                    Console.WriteLine($"Shape: {shapeName}, Paragraphs: {paragraphCount}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
