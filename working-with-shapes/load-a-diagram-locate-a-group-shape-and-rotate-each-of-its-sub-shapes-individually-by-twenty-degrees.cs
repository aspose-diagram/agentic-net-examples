using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Locate the first group shape on the page
            Shape groupShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Type == TypeValue.Group && shape.Del == BOOL.False)
                {
                    groupShape = shape;
                    break;
                }
            }

            if (groupShape == null)
            {
                Console.WriteLine("No group shape found on the page.");
                return;
            }

            // Rotate each sub‑shape within the group by 20 degrees
            foreach (Shape subShape in groupShape.Shapes)
            {
                if (subShape.Del == BOOL.False)
                {
                    double currentAngle = subShape.XForm.Angle.Value;
                    subShape.XForm.Angle.Value = currentAngle + 20.0;
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved with rotated sub‑shapes.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
