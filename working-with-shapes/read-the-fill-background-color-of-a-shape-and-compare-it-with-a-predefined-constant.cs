using System;
using Aspose.Diagram;

class Program
    {
        // Predefined constant for the expected fill background color (hex string)
        private const string ExpectedFillColor = "#FF0000";

        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Example: target shape ID (adjust as needed)
                long targetShapeId = 1;

                // Retrieve the first page (index 0)
                Page page = diagram.Pages[0];

                // Get the shape by ID (cast to int if required by the overload)
                Shape shape = page.Shapes.GetShape((int)targetShapeId);

                if (shape == null)
                {
                    throw new Exception($"Shape with ID {targetShapeId} not found.");
                }

                // Read the fill background color (hex string) from the shape
                string actualFillColor = shape.Fill.FillBkgnd.Value;

                // Compare with the predefined constant (case‑insensitive)
                if (string.Equals(actualFillColor, ExpectedFillColor, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Shape ID {targetShapeId} has the expected fill background color: {actualFillColor}");
                }
                else
                {
                    throw new Exception($"Fill background color mismatch. Expected: {ExpectedFillColor}, Actual: {actualFillColor}");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }