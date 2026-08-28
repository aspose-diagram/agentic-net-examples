using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Locate the source and target shapes by their names
            Shape sourceShape = null;
            Shape targetShape = null;

            foreach (Shape shape in page.Shapes)
            {
                if (shape.Name == "SourceShape")
                    sourceShape = shape;
                else if (shape.Name == "TargetShape")
                    targetShape = shape;
            }

            // Ensure both shapes were found before proceeding
            if (sourceShape != null && targetShape != null)
            {
                // Clone all protection settings from the source shape to the target shape
                targetShape.Copy(sourceShape);
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
