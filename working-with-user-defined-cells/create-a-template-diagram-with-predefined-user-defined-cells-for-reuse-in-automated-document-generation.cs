using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define output path for the template diagram
        string outputPath = "TemplateDiagram.vsdx";

        // Guard: ensure we can write to the directory
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram
            using (Diagram diagram = new Diagram())
            {
                // Ensure the diagram has at least one window; add if missing
                if (diagram.Windows.Count == 0)
                {
                    Window window = new Window();
                    window.WindowState = WindowStateValue.Maximized;
                    window.WindowWidth = 1100;
                    window.WindowHeight = 700;
                    window.WindowType = WindowTypeValue.Drawing;
                    diagram.Windows.Add(window);
                }

                // Create a new page for the template
                Page templatePage = new Page();
                templatePage.Name = "TemplatePage";
                // Assign a unique ID (simple increment based on existing pages)
                templatePage.ID = diagram.Pages.Count + 1;
                diagram.Pages.Add(templatePage);

                // Add a rectangle shape to the page (using a built‑in master)
                double pinX = 2.0;
                double pinY = 2.0;
                string masterName = "Rectangle";
                long shapeId = templatePage.AddShape(pinX, pinY, masterName);
                Shape shape = templatePage.Shapes.GetShape(shapeId);

                // ----- Define user‑defined cells for the shape -----
                // First custom cell
                User userCell1 = new User();
                userCell1.Name = "UserField1";
                userCell1.Value.Val = "SampleValue1";
                userCell1.Prompt.Value = "Enter value for UserField1";
                shape.Users.Add(userCell1);

                // Second custom cell
                User userCell2 = new User();
                userCell2.Name = "UserField2";
                userCell2.Value.Val = "SampleValue2";
                userCell2.Prompt.Value = "Enter value for UserField2";
                shape.Users.Add(userCell2);

                // Save the diagram as a VSDX template
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Template diagram saved successfully to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while creating the template diagram: {ex.Message}");
        }
    }
}