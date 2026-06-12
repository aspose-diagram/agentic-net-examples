using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                Console.WriteLine($"Page: {page.Name}");

                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Use Name if set, otherwise fall back to the universal name
                    string shapeName = !string.IsNullOrEmpty(shape.Name) ? shape.Name : shape.NameU;

                    // Count the paragraphs contained in the shape's text
                    int paragraphCount = shape.Paras.Count;

                    Console.WriteLine($"  Shape: {shapeName}, Paragraphs: {paragraphCount}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
