using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Add a new page to the diagram
                Page page = new Page();
                diagram.Pages.Add(page);

                // Apply a preset theme to the page.
                // The specific "flowchart" theme is not available in the enum,
                // so we use a valid theme (Bubble) as an example.
                page.PresetTheme = PresetThemeValue.Bubble;

                // Define position for the rectangle shape (in inches)
                double pinX = 2.0;
                double pinY = 2.0;
                string masterName = "Rectangle";

                // Add the rectangle shape to the page
                long shapeId = page.AddShape(pinX, pinY, masterName);
                Shape rectangle = page.Shapes.GetShape(shapeId);

                // Optionally add some text to the rectangle
                rectangle.Text.Value.Add(new Txt("Rectangle"));

                // Save the diagram to a VSDX file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }