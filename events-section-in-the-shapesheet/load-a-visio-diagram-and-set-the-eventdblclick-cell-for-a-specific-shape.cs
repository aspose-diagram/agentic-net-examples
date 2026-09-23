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

            // Path for the modified Visio file
            string outputPath = "output.vsdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // ID of the shape whose double‑click event you want to set
            // Replace this with the actual shape ID in your diagram
            long shapeId = 1;

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Assign a Visio formula to the double‑click event cell
            shape.Event.EventDblClick.Ufe.F = "CALLTHIS(\"MyMacro\")";

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
