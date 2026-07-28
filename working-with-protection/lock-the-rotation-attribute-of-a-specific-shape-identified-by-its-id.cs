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

            // Path to save the modified file
            string outputPath = "output_locked.vsdx";

            // ID of the shape whose rotation should be locked (replace with actual ID)
            long targetShapeId = 5;

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the shape by ID across all pages
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                try
                {
                    Shape shape = page.Shapes.GetShape(targetShapeId);
                    if (shape != null)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                catch
                {
                    // Shape not on this page; continue searching
                }
            }

            if (targetShape == null)
            {
                throw new Exception($"Shape with ID {targetShapeId} not found in the diagram.");
            }

            // Lock the rotation attribute of the shape
            targetShape.Protection.LockRotate.Value = BOOL.True;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
