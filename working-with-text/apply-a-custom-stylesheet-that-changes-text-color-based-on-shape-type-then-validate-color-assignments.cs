using System.IO;
using System;
using Aspose.Diagram;

class ApplyCustomStyleAndValidate
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all shapes on the first page
            Page page = diagram.Pages[0];
            foreach (Shape shape in page.Shapes)
            {
                // Determine shape type (e.g., based on its Name)
                // For demonstration: rectangles get Red, ellipses get Blue, others get Green
                PresetColorMatricsValue colorIndex;
                if (shape.NameU != null && shape.NameU.Contains("Rectangle"))
                {
                    // Red (Color1)
                    colorIndex = PresetColorMatricsValue.Color1;
                }
                else if (shape.NameU != null && shape.NameU.Contains("Ellipse"))
                {
                    // Blue (Color2)
                    colorIndex = PresetColorMatricsValue.Color2;
                }
                else
                {
                    // Green (Color3)
                    colorIndex = PresetColorMatricsValue.Color3;
                }

                // Apply a preset theme style matrix with the chosen color.
                // Using Style1 as a generic style row.
                shape.SetPresetThemeStyleMatrics(PresetStyleMatricsValue.Style1, colorIndex);

                // Store the assigned color index in Data1 for later validation
                shape.Data1 = ((int)colorIndex).ToString();
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            // Validation: read back the file and verify color assignments
            Diagram validationDiagram = new Diagram("output.vsdx");
            Page validationPage = validationDiagram.Pages[0];
            foreach (Shape shape in validationPage.Shapes)
            {
                // Retrieve the stored color index
                int storedColor;
                if (int.TryParse(shape.Data1, out storedColor))
                {
                    // Compare with expected logic based on shape name
                    int expectedColor;
                    if (shape.NameU != null && shape.NameU.Contains("Rectangle"))
                        expectedColor = (int)PresetColorMatricsValue.Color1;
                    else if (shape.NameU != null && shape.NameU.Contains("Ellipse"))
                        expectedColor = (int)PresetColorMatricsValue.Color2;
                    else
                        expectedColor = (int)PresetColorMatricsValue.Color3;

                    if (storedColor == expectedColor)
                    {
                        Console.WriteLine($"Shape ID {shape.ID} ({shape.NameU}) color assignment validated.");
                    }
                    else
                    {
                        Console.WriteLine($"Shape ID {shape.ID} ({shape.NameU}) color mismatch! Expected {expectedColor}, found {storedColor}.");
                    }
                }
                else
                {
                    Console.WriteLine($"Shape ID {shape.ID} ({shape.NameU}) has no stored color data.");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
