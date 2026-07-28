using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Use the first page (index 0) as per guidelines
                Page page = diagram.Pages[0];

                // Name of the shape to locate
                string targetShapeName = "MyShape";

                // Flag to indicate if the shape was found
                bool shapeFound = false;

                // Iterate through all shapes on the page
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Compare the universal name of the shape
                    if (shape.NameU == targetShapeName)
                    {
                        shapeFound = true;

                        // Retrieve LocPinY, PinY and Height values
                        double locPinY = shape.XForm.LocPinY.Value;
                        double pinY = shape.XForm.PinY.Value;
                        double height = shape.XForm.Height.Value;

                        // Compute absolute PinY of the local pin
                        // Formula: absolutePinY = PinY + (LocPinY - 0.5) * Height
                        double absolutePinY = pinY + (locPinY - 0.5) * height;

                        Console.WriteLine($"Shape '{targetShapeName}' absolute PinY: {absolutePinY}");
                        break;
                    }
                }

                if (!shapeFound)
                {
                    throw new Exception($"Shape with name '{targetShapeName}' was not found on the page.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }