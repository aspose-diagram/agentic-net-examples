using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load the Visio diagram (replace with your actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // Retrieve two shapes by their IDs (replace with actual IDs)
                long shapeId1 = 1; // Example ID for the first shape
                long shapeId2 = 2; // Example ID for the second shape

                Shape shape1 = page.Shapes.GetShape(shapeId1);
                Shape shape2 = page.Shapes.GetShape(shapeId2);

                // Get the fill foreground colors (hex strings, e.g., "#FF0000")
                string fillColor1 = shape1.Fill.FillForegnd.Value;
                string fillColor2 = shape2.Fill.FillForegnd.Value;

                // Compare the colors and log a warning if they differ
                if (!string.Equals(fillColor1, fillColor2, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Warning: Fill colors differ between shapes.");
                    Console.WriteLine($"Shape ID {shape1.ID} Fill: {fillColor1}");
                    Console.WriteLine($"Shape ID {shape2.ID} Fill: {fillColor2}");
                }
                else
                {
                    Console.WriteLine("Fill colors are identical.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }