using System;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Work with the first page (index 0)
                Page page = diagram.Pages[0];

                // Configure auto‑spacing options (example distances)
                AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 1.0, // inches
                    DistanceInVertical = 1.0    // inches
                };

                // Apply auto‑spacing to all shapes on the page
                page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);

                // Expected positions for validation (shape ID -> (expected PinX, expected PinY))
                var expectedPositions = new System.Collections.Generic.Dictionary<long, (double PinX, double PinY)>
                {
                    // Example entries – replace with real expected values
                    { 1, (2.0, 3.0) },
                    { 2, (5.0, 4.0) }
                };

                // Tolerance for floating‑point comparison (in inches)
                const double tolerance = 0.001;

                // Validate each shape's position
                foreach (Shape shape in page.Shapes)
                {
                    long shapeId = shape.ID;

                    if (expectedPositions.TryGetValue(shapeId, out var expected))
                    {
                        double actualPinX = shape.XForm.PinX.Value;
                        double actualPinY = shape.XForm.PinY.Value;

                        bool xMatches = Math.Abs(actualPinX - expected.PinX) <= tolerance;
                        bool yMatches = Math.Abs(actualPinY - expected.PinY) <= tolerance;

                        if (!xMatches || !yMatches)
                        {
                            string message = $"Shape ID {shapeId} position mismatch. " +
                                             $"Expected (PinX={expected.PinX}, PinY={expected.PinY}), " +
                                             $"Actual (PinX={actualPinX}, PinY={actualPinY}).";
                            // Throwing an exception signals validation failure
                            throw new Exception(message);
                        }
                        else
                        {
                            Console.WriteLine($"Shape ID {shapeId} position validated successfully.");
                        }
                    }
                    else
                    {
                        // No expected position defined for this shape; skip validation
                        Console.WriteLine($"Shape ID {shapeId} has no expected position defined; skipping.");
                    }
                }

                // Save the diagram after auto‑spacing (optional)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Auto‑spacing validation completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }