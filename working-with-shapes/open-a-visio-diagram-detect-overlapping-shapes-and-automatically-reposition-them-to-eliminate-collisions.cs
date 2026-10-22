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
        // Input and output file paths (command‑line arguments or defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Process each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // -------------------------------------------------------------
                    // Detect overlapping shapes on the current page
                    // -------------------------------------------------------------
                    List<(long, long)> overlappingPairs = new List<(long, long)>();
                    List<Shape> shapeList = new List<Shape>();

                    // Collect non‑deleted, visible shapes with non‑zero size
                    foreach (Shape s in page.Shapes)
                    {
                        if (s.Del == BOOL.True) continue; // skip deleted shapes
                        if (s.XForm.Width.Value == 0 || s.XForm.Height.Value == 0) continue; // skip zero‑size shapes
                        shapeList.Add(s);
                    }

                    // Compare each pair of shapes for intersection
                    for (int i = 0; i < shapeList.Count; i++)
                    {
                        Shape a = shapeList[i];
                        double aLeft   = a.XForm.PinX.Value - a.XForm.Width.Value / 2.0;
                        double aRight  = a.XForm.PinX.Value + a.XForm.Width.Value / 2.0;
                        double aTop    = a.XForm.PinY.Value + a.XForm.Height.Value / 2.0;
                        double aBottom = a.XForm.PinY.Value - a.XForm.Height.Value / 2.0;

                        for (int j = i + 1; j < shapeList.Count; j++)
                        {
                            Shape b = shapeList[j];
                            double bLeft   = b.XForm.PinX.Value - b.XForm.Width.Value / 2.0;
                            double bRight  = b.XForm.PinX.Value + b.XForm.Width.Value / 2.0;
                            double bTop    = b.XForm.PinY.Value + b.XForm.Height.Value / 2.0;
                            double bBottom = b.XForm.PinY.Value - b.XForm.Height.Value / 2.0;

                            bool overlapX = aLeft < bRight && aRight > bLeft;
                            bool overlapY = aBottom < bTop && aTop > bBottom;

                            if (overlapX && overlapY)
                            {
                                overlappingPairs.Add((a.ID, b.ID));
                            }
                        }
                    }

                    // Report overlapping shape pairs
                    if (overlappingPairs.Count > 0)
                    {
                        Console.WriteLine($"Page \"{page.NameU}\" has {overlappingPairs.Count} overlapping shape pairs:");
                        foreach (var pair in overlappingPairs)
                        {
                            Console.WriteLine($"  Shape ID {pair.Item1} overlaps with Shape ID {pair.Item2}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Page \"{page.NameU}\" has no overlapping shapes.");
                    }

                    // -------------------------------------------------------------
                    // Reposition shapes to eliminate collisions using AutoSpaceShapes
                    // -------------------------------------------------------------
                    AutoSpaceOptions options = new AutoSpaceOptions();
                    // Set spacing distances (in inches) – adjust as needed
                    options.DistanceInHorizontal = 0.5;
                    options.DistanceInVertical   = 0.5;

                    // Auto‑space all shapes on the page
                    page.AutoSpaceShapes(page.Shapes, options);
                }

                // Save the adjusted diagram to the output file (must specify format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}