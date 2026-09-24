using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram from file
                Diagram diagram = new Diagram("input.vsdx");

                // Get the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // Identify the shape to export (replace with the actual shape ID)
                int shapeId = 1;
                Shape shape = page.Shapes.GetShape(shapeId);

                // Prepare HTML export options
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                // Export the selected shape to HTML, preserving geometry and style
                string outputPath = "shape.html";
                shape.ToHTML(outputPath, htmlOptions);

                Console.WriteLine($"Shape {shapeId} exported to HTML at '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }