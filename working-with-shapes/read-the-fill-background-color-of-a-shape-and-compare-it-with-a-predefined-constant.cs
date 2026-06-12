using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the page index (0‑based) and shape ID to inspect
                int pageIndex = 0;
                long shapeId = 1; // replace with the actual shape ID you want to check

                // Retrieve the page
                Page page = diagram.Pages[pageIndex];

                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Predefined fill background color constant (hex string)
                const string ExpectedFillColor = "#FF0000";

                // Read the shape's fill background color
                string actualFillColor = shape.Fill.FillBkgnd.Value;

                // Compare and act accordingly
                if (string.Equals(actualFillColor, ExpectedFillColor, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("The shape's fill background color matches the expected value.");
                }
                else
                {
                    throw new Exception($"Fill color mismatch. Expected: {ExpectedFillColor}, Actual: {actualFillColor}");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }