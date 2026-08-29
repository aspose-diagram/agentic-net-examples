using System;
using Aspose.Diagram;

class Program
    {
        // Predefined constant color in hexadecimal format (Visio uses hex strings for colors)
        private const string ExpectedFillColor = "#FF0000";

        static void Main()
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string diagramPath = "input.vsdx";

                // Load the diagram from file
                Diagram diagram = new Diagram(diagramPath);

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve a shape by its ID.
                // Here we assume a shape with ID 1 exists; replace with the actual ID as required.
                Shape shape = page.Shapes.GetShape(1);

                // Read the background fill color of the shape.
                // FillBkgnd holds the background color as a hex string (e.g., "#FF0000").
                string actualFillColor = shape.Fill.FillBkgnd.Value;

                // Compare the retrieved color with the predefined constant.
                if (string.Equals(actualFillColor, ExpectedFillColor, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("The shape's fill background color matches the expected value.");
                }
                else
                {
                    // Throw an exception to indicate the mismatch (as per the project's error handling policy).
                    throw new Exception($"Fill color mismatch. Expected: {ExpectedFillColor}, Actual: {actualFillColor}");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }