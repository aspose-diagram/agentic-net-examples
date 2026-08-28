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
            // Path where the modified Visio file will be saved
            string outputPath = "output.vsdx";

            // Load the diagram from file
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Get the first page (you can also retrieve by name: diagram.Pages.GetPage("Page-1"))
                Page page = diagram.Pages[0];

                // Identify the target shape.
                // Example: find a shape whose universal name is "MyShape".
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == "MyShape")
                    {
                        targetShape = shape;
                        break;
                    }
                }

                // If the shape was not found, fall back to a known shape ID (e.g., 1)
                if (targetShape == null)
                {
                    // Replace 1 with the actual shape ID you want to comment on
                    long shapeId = 1;
                    targetShape = page.Shapes.GetShape(shapeId);
                }

                // Add a comment to the identified shape
                page.AddComment(targetShape, "Review this shape for accuracy.");

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram updated and saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
