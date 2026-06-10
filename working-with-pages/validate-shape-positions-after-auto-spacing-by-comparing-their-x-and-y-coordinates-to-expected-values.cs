using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram (replace with actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Get the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // Configure auto‑spacing options
                AutoSpaceOptions options = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 1.0, // example horizontal spacing
                    DistanceInVertical = 1.0    // example vertical spacing
                };

                // Apply auto‑spacing to all shapes on the page
                page.AutoSpaceShapes(page.Shapes, options);

                // Expected positions after auto‑spacing (shape ID -> (PinX, PinY))
                // Populate with the IDs and coordinates you expect.
                var expectedPositions = new Dictionary<long, (double X, double Y)>
                {
                    // Example entries:
                    // { 1, (2.5, 3.0) },
                    // { 2, (5.0, 3.0) }
                };

                const double tolerance = 0.001; // allowable deviation

                // Validate each shape's position against the expected values
                foreach (Shape shape in page.Shapes)
                {
                    if (expectedPositions.TryGetValue(shape.ID, out var expected))
                    {
                        double actualX = shape.XForm.PinX.Value;
                        double actualY = shape.XForm.PinY.Value;

                        bool xMatch = Math.Abs(actualX - expected.X) <= tolerance;
                        bool yMatch = Math.Abs(actualY - expected.Y) <= tolerance;

                        if (!xMatch || !yMatch)
                        {
                            throw new Exception(
                                $"Shape ID {shape.ID} position mismatch. " +
                                $"Expected (X={expected.X}, Y={expected.Y}), " +
                                $"Actual (X={actualX}, Y={actualY}).");
                        }
                        else
                        {
                            Console.WriteLine(
                                $"Shape ID {shape.ID} position validated: " +
                                $"X={actualX}, Y={actualY}");
                        }
                    }
                    else
                    {
                        // No expected position defined for this shape; skip or log as needed
                        Console.WriteLine($"Shape ID {shape.ID} has no expected position defined.");
                    }
                }

                Console.WriteLine("All defined shape positions validated successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }