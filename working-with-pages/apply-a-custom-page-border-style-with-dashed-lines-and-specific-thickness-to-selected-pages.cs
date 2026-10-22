using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input file, output file, comma‑separated page indices
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> <pageIndices>");
            Console.Error.WriteLine("Example: Program diagram.vsdx diagram_modified.vsdx 0,2,3");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string pageIndicesArg = args[2];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse page indices
        List<int> pageIndices = new List<int>();
        try
        {
            foreach (var part in pageIndicesArg.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                pageIndices.Add(int.Parse(part.Trim()));
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse page indices: {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each requested page
            foreach (int pageIndex in pageIndices)
            {
                // Validate page index range
                if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
                {
                    Console.Error.WriteLine($"Page index out of range: {pageIndex}");
                    continue; // skip invalid index
                }

                // Retrieve the page
                Page page = diagram.Pages[pageIndex];

                // Get page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Define rectangle points for the border (closed polyline)
                double[] borderPoints = new double[]
                {
                    0, 0,                     // Bottom‑left
                    pageWidth, 0,             // Bottom‑right
                    pageWidth, pageHeight,    // Top‑right
                    0, pageHeight,            // Top‑left
                    0, 0                      // Close the shape
                };

                // Draw the polyline on the page; returns the shape ID
                long shapeId = page.DrawPolyline(borderPoints);

                // Retrieve the created shape object
                Shape borderShape = page.Shapes.GetShape(shapeId);

                // Apply dashed line pattern
                borderShape.Line.LinePattern.Value = LinePatternValue.Dash;

                // Set line thickness (weight) – value is in inches
                borderShape.Line.LineWeight.Value = 0.02; // example thickness

                // Optional: set line color (black)
                borderShape.Line.LineColor.Value = "#000000";

                // Ensure the shape has no fill
                borderShape.Fill.FillPattern.Value = 0; // 0 = No fill
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or runtime errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}