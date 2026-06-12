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
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Locate the first group shape on the page
            Shape groupShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Type == TypeValue.Group)
                {
                    groupShape = shape;
                    break;
                }
            }

            if (groupShape == null)
            {
                throw new Exception("No group shape found in the diagram.");
            }

            // Verify the group contains at least one sub‑shape
            if (groupShape.Shapes.Count == 0)
            {
                throw new Exception("The group shape does not contain any sub‑shapes.");
            }

            // Retrieve the first sub‑shape within the group
            Shape subShape = groupShape.Shapes[0];

            // Adjust the width of the sub‑shape to 5.0 inches using SetWidth (double precision)
            subShape.SetWidth(5.0);

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
