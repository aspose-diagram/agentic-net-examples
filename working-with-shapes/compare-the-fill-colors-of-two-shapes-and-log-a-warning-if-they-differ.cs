using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram (replace with your actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Assume we are working with the first page
                Page page = diagram.Pages[0];

                // IDs of the two shapes to compare (replace with actual IDs)
                long shapeId1 = 1;
                long shapeId2 = 2;

                // Retrieve the shapes by their IDs
                Shape shape1 = page.Shapes.GetShape(shapeId1);
                Shape shape2 = page.Shapes.GetShape(shapeId2);

                // Get the foreground fill colors (hex string, e.g., "#FF0000")
                string fillColor1 = shape1.Fill.FillForegnd.Value;
                string fillColor2 = shape2.Fill.FillForegnd.Value;

                // Compare the colors and log a warning if they differ
                if (!string.Equals(fillColor1, fillColor2, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Warning: Fill colors differ. Shape {shapeId1} has {fillColor1}, Shape {shapeId2} has {fillColor2}.");
                }
                else
                {
                    Console.WriteLine($"Info: Fill colors are identical ({fillColor1}).");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }