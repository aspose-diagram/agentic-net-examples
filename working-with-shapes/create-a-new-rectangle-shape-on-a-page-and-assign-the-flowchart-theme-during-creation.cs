using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new blank diagram
            using (Diagram diagram = new Diagram())
            {
                // Add a new page to the diagram
                Page page = new Page();
                diagram.Pages.Add(page);

                // Apply a preset theme to the page (the closest available theme)
                page.PresetTheme = PresetThemeValue.Bubble;

                // Add a rectangle shape at position (2, 2) on the page
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);
                Shape rectangle = page.Shapes.GetShape((int)shapeId);

                // Optionally add some text to the rectangle
                rectangle.Text.Value.Add(new Txt("Rectangle"));

                // Save the diagram to a VSDX file
                diagram.Save("FlowchartDiagram.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
