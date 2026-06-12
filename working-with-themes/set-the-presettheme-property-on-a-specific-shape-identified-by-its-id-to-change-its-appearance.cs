using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // ID of the shape to modify
            int targetShapeId = 5; // TODO: replace with the actual shape ID

            // Locate the shape by its ID across all pages
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.ID == targetShapeId)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null) break;
            }

            if (targetShape != null)
            {
                // Apply a preset theme to the found shape
                targetShape.PresetTheme = PresetThemeValue.Office; // choose desired theme
            }
            else
            {
                Console.WriteLine($"Shape with ID {targetShapeId} not found.");
            }

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
