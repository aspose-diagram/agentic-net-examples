using System.IO;
using System;
using Aspose.Diagram;

using Aspose.Diagram.Manipulation; // Required for ConnectionPointPlace if needed

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes to find connector shapes (1‑D shapes)
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Connectors are 1‑D shapes (OneD == true)
                    if (shape.OneD)
                    {
                        // Set the target (end) arrowhead to a filled triangle.
                        // Arrow style value 4 corresponds to a filled triangle in Visio.
                        shape.Line.EndArrow.Value = 4;

                        // Optionally set the arrow size (e.g., Large). Adjust as needed.
                        shape.Line.EndArrowSize.Value = ArrowSizeValue.Large;
                    }
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
