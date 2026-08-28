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

            // Access the first page (you can adjust the index as needed)
            Page page = diagram.Pages[0];

            // Iterate through shapes to find a circle (ellipse master)
            foreach (Aspose.Diagram.Shape shape in page.Shapes)
            {
                // The master name for a circle/ellipse is typically "Ellipse"
                if (shape.Master != null && shape.Master.Name == "Ellipse")
                {
                    // Aspose.Diagram does not expose a LineJoin property.
                    // To achieve sharper corners on the outline, set the rounding to zero.
                    shape.Line.Rounding.Value = 0;
                    Console.WriteLine($"Shape ID {shape.ID}: line rounding set to 0 (sharp corners).");
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
