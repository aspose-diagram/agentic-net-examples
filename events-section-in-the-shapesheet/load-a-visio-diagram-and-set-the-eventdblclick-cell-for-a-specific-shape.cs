using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // NameU of the shape whose double‑click event we want to set
            string targetShapeNameU = "MyShape";

            Shape targetShape = null;

            // Search for the shape by its universal name across all pages
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == targetShapeNameU)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                Console.WriteLine($"Shape with NameU '{targetShapeNameU}' not found.");
                return;
            }

            // Set the double‑click event formula
            targetShape.Event.EventDblClick.Ufe.F = "CALLTHIS(\"ShowAlert\")";

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved with updated double‑click event.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
