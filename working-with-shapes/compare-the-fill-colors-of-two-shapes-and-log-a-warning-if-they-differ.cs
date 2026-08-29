using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load the Visio diagram (replace with your actual file path)
                string diagramPath = "input.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Define the IDs of the two shapes to compare
                // Replace these IDs with the actual shape IDs you want to compare
                long shapeId1 = 1;
                long shapeId2 = 2;

                // Retrieve the shapes from the first page (index 0)
                Shape shape1 = diagram.Pages[0].Shapes.GetShape(shapeId1);
                Shape shape2 = diagram.Pages[0].Shapes.GetShape(shapeId2);

                // Ensure both shapes were found
                if (shape1 == null || shape2 == null)
                {
                    Console.WriteLine("One or both shapes could not be found.");
                    return;
                }

                // Get the foreground fill color values (hex strings, e.g., "#FF0000")
                string fillColor1 = shape1.Fill.FillForegnd.Value;
                string fillColor2 = shape2.Fill.FillForegnd.Value;

                // Compare the fill colors and log a warning if they differ
                if (!string.Equals(fillColor1, fillColor2, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Warning: Shape {shapeId1} fill color ({fillColor1}) differs from shape {shapeId2} fill color ({fillColor2}).");
                }
                else
                {
                    Console.WriteLine($"Shapes {shapeId1} and {shapeId2} have matching fill colors ({fillColor1}).");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }