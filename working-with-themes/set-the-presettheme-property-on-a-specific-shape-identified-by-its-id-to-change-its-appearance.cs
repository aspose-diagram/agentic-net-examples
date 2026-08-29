using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // ID of the shape whose theme we want to change
            int targetShapeId = 5; // replace with the actual shape ID

            // Iterate through pages and shapes to locate the shape by ID
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.ID == targetShapeId)
                    {
                        // Apply a preset theme (e.g., Office) to the shape
                        shape.PresetTheme = PresetThemeValue.Office;
                        // If you need a different theme, use another PresetThemeValue enum member
                        break; // shape found, exit inner loop
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
