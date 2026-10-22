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
        // Path to the input Visio diagram
        string diagramPath = "sample.vsdx";
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Collect all non-deleted shapes on the page (optional, just for reference)
                List<Shape> shapeList = new List<Shape>();
                foreach (Shape s in page.Shapes)
                {
                    if (s.Del == BOOL.False)
                    {
                        shapeList.Add(s);
                    }
                }

                // Configure auto‑spacing options (horizontal and vertical distances in inches)
                AutoSpaceOptions options = new AutoSpaceOptions();
                // The correct property names are DistanceInHorizontal and DistanceInVertical
                options.DistanceInHorizontal = 0.5; // 0.5 inches between shapes horizontally
                options.DistanceInVertical = 0.5;   // 0.5 inches between shapes vertically

                // Apply auto‑spacing to the entire shape collection on the page
                page.AutoSpaceShapes(page.Shapes, options);

                // Expected coordinates after auto‑spacing (example values)
                var expectedPositions = new Dictionary<string, (double PinX, double PinY)>
                {
                    { "Rectangle", (2.0, 2.0) },
                    { "Process",   (4.0, 2.0) }
                };

                // Verify each expected shape's position
                const double tolerance = 0.01; // allowable deviation in inches
                foreach (var kvp in expectedPositions)
                {
                    string shapeName = kvp.Key;
                    double expectedX = kvp.Value.PinX;
                    double expectedY = kvp.Value.PinY;

                    Shape targetShape = null;
                    foreach (Shape s in page.Shapes)
                    {
                        if (s.Del == BOOL.False && s.NameU == shapeName)
                        {
                            targetShape = s;
                            break;
                        }
                    }

                    if (targetShape == null)
                    {
                        throw new Exception($"Shape '{shapeName}' not found on the page.");
                    }

                    double actualX = targetShape.XForm.PinX.Value;
                    double actualY = targetShape.XForm.PinY.Value;

                    if (Math.Abs(actualX - expectedX) > tolerance || Math.Abs(actualY - expectedY) > tolerance)
                    {
                        throw new Exception(
                            $"Position mismatch for shape '{shapeName}'. Expected ({expectedX}, {expectedY}) but got ({actualX}, {actualY}).");
                    }
                    else
                    {
                        Console.WriteLine($"Shape '{shapeName}' position verified: ({actualX}, {actualY}).");
                    }
                }

                // Save the modified diagram (optional)
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}