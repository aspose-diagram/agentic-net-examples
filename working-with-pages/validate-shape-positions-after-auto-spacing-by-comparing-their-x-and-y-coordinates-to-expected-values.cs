using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (required)
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Expected positions file path (required). Each line: <ShapeId> <PinX> <PinY>
        string expectedPath = args.Length > 1 ? args[1] : "expected.txt";
        if (!File.Exists(expectedPath))
        {
            Console.Error.WriteLine($"Expected positions file not found: {expectedPath}");
            return;
        }

        // Load expected positions into a dictionary for quick lookup
        var expectedPositions = new Dictionary<long, (double PinX, double PinY)>();
        try
        {
            foreach (var line in File.ReadAllLines(expectedPath))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 3)
                    continue; // skip malformed lines

                if (long.TryParse(parts[0], out long shapeId) &&
                    double.TryParse(parts[1], out double pinX) &&
                    double.TryParse(parts[2], out double pinY))
                {
                    expectedPositions[shapeId] = (pinX, pinY);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading expected positions: {ex.Message}");
            return;
        }

        try
        {
            // Load the diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Ensure the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Diagram contains no pages.");
                    return;
                }

                // Work with the first page
                Page page = diagram.Pages[0];

                // Prepare AutoSpaceOptions (set horizontal and vertical distances)
                AutoSpaceOptions options = new AutoSpaceOptions();
                options.DistanceInHorizontal = 0.5; // inches
                options.DistanceInVertical = 0.5;   // inches

                // Perform auto-spacing on all shapes in the page
                page.AutoSpaceShapes(page.Shapes, options);

                // Validate each shape's position against expected values
                const double tolerance = 0.001; // acceptable deviation in inches
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    long shapeId = shape.ID;
                    if (!expectedPositions.TryGetValue(shapeId, out var expected))
                    {
                        // No expectation defined for this shape; skip validation
                        continue;
                    }

                    double actualPinX = shape.XForm.PinX.Value;
                    double actualPinY = shape.XForm.PinY.Value;

                    bool xMatches = Math.Abs(actualPinX - expected.PinX) <= tolerance;
                    bool yMatches = Math.Abs(actualPinY - expected.PinY) <= tolerance;

                    if (!xMatches || !yMatches)
                    {
                        Console.Error.WriteLine($"Shape ID {shapeId} position mismatch:");
                        Console.Error.WriteLine($"  Expected PinX={expected.PinX}, Actual PinX={actualPinX}");
                        Console.Error.WriteLine($"  Expected PinY={expected.PinY}, Actual PinY={actualPinY}");
                    }
                    else
                    {
                        Console.WriteLine($"Shape ID {shapeId} position validated successfully.");
                    }
                }

                // Save the modified diagram (optional)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}