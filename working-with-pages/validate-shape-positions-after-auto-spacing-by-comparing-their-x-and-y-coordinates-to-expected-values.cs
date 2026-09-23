using System;
using System.Collections.Generic;
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

                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Configure auto‑spacing options (example distances)
                AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 1.0, // inches
                    DistanceInVertical = 1.0    // inches
                };

                // Apply auto‑spacing to all shapes on the page
                page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);

                // Expected positions after auto‑spacing (shape ID -> (PinX, PinY))
                // These values should be set according to your test expectations.
                var expectedPositions = new Dictionary<long, (double X, double Y)>
                {
                    // Example entries:
                    // { shapeId, (expectedPinX, expectedPinY) }
                    { 1, (2.0, 3.0) },
                    { 2, (4.0, 5.0) },
                    // Add more expected positions as needed
                };

                const double tolerance = 0.001; // allowable deviation in inches

                // Validate each shape's position
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    long shapeId = shape.ID;

                    if (expectedPositions.TryGetValue(shapeId, out var expected))
                    {
                        double actualX = shape.XForm.PinX.Value;
                        double actualY = shape.XForm.PinY.Value;

                        bool xMatches = Math.Abs(actualX - expected.X) <= tolerance;
                        bool yMatches = Math.Abs(actualY - expected.Y) <= tolerance;

                        if (!xMatches || !yMatches)
                        {
                            string message = $"Shape ID {shapeId} position mismatch. " +
                                             $"Expected (X={expected.X}, Y={expected.Y}), " +
                                             $"Actual (X={actualX}, Y={actualY}).";
                            Console.WriteLine(message);
                            throw new Exception(message);
                        }
                        else
                        {
                            Console.WriteLine($"Shape ID {shapeId} position validated successfully.");
                        }
                    }
                    else
                    {
                        // No expected position defined for this shape; optionally log it.
                        Console.WriteLine($"No expected position defined for shape ID {shapeId}; skipping validation.");
                    }
                }

                // Optionally save the diagram after validation
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Validation completed and diagram saved.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }