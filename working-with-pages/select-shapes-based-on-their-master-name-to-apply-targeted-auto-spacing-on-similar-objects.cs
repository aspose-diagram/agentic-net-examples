using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Master name to filter shapes
        string targetMasterName = "Rectangle";

        // Output Visio file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Store original positions of all shapes (to restore non-target shapes later)
                Dictionary<long, (double PinX, double PinY)> originalPositions = new Dictionary<long, (double, double)>();
                // Keep IDs of shapes that match the target master name
                HashSet<long> targetShapeIds = new HashSet<long>();

                // First pass: collect target shapes and record positions of all shapes
                foreach (Shape shape in page.Shapes)
                {
                    // Record current position for every shape
                    originalPositions[shape.ID] = (shape.XForm.PinX.Value, shape.XForm.PinY.Value);

                    // Ensure the shape has a master before accessing its name
                    if (shape.Master != null && shape.Master.Name == targetMasterName)
                    {
                        targetShapeIds.Add(shape.ID);
                    }
                }

                // Configure auto‑spacing options (horizontal and vertical gaps)
                AutoSpaceOptions options = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 1.0, // 1 inch gap horizontally
                    DistanceInVertical = 1.0    // 1 inch gap vertically
                };

                // Apply auto‑spacing to all shapes on the page
                page.AutoSpaceShapes(page.Shapes, options);

                // Second pass: restore positions of shapes that are NOT in the target set
                foreach (Shape shape in page.Shapes)
                {
                    if (!targetShapeIds.Contains(shape.ID) && originalPositions.TryGetValue(shape.ID, out var pos))
                    {
                        shape.XForm.PinX.Value = pos.PinX;
                        shape.XForm.PinY.Value = pos.PinY;
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}