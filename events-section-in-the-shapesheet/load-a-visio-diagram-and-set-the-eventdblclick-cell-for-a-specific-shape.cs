using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the existing Visio file
            string inputPath = "input.vsdx";
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Identify the target shape by its universal name (NameU)
            // Replace "TargetShapeName" with the actual shape NameU you want to modify
            string targetShapeNameU = "TargetShapeName";
            Aspose.Diagram.Shape targetShape = null;

            foreach (Aspose.Diagram.Shape shape in page.Shapes)
            {
                if (shape.NameU == targetShapeNameU)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                throw new Exception($"Shape with NameU '{targetShapeNameU}' not found on the first page.");
            }

            // Set the double‑click event formula.
            // Example formula calls a macro named "MyMacro" in the Visio document.
            targetShape.Event.EventDblClick.Ufe.F = "CALLTHIS(\"MyMacro\")";

            // Save the modified diagram to a new file
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
