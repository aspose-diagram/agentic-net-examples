using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least input and output file paths.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> [pageIndices(comma separated)]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse optional page indices.
        int[] selectedPageIndices = null;
        if (args.Length >= 3)
        {
            try
            {
                string[] parts = args[2].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                selectedPageIndices = new int[parts.Length];
                for (int i = 0; i < parts.Length; i++)
                {
                    selectedPageIndices[i] = int.Parse(parts[i].Trim());
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to parse page indices: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Determine which pages to process.
            int pageCount = diagram.Pages.Count;
            var pagesToProcess = new System.Collections.Generic.List<Page>();

            if (selectedPageIndices == null || selectedPageIndices.Length == 0)
            {
                // No specific indices provided – process all pages.
                for (int i = 0; i < pageCount; i++)
                {
                    pagesToProcess.Add(diagram.Pages[i]);
                }
            }
            else
            {
                // Process only the specified pages.
                foreach (int idx in selectedPageIndices)
                {
                    if (idx < 0 || idx >= pageCount)
                    {
                        Console.Error.WriteLine($"Page index out of range: {idx}");
                        continue;
                    }
                    pagesToProcess.Add(diagram.Pages[idx]);
                }
            }

            // Apply custom border to each selected page.
            foreach (Page page in pagesToProcess)
            {
                // Retrieve page dimensions (in inches).
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Define rectangle points for the border (starting at 0,0).
                double[] borderPoints = new double[]
                {
                    0, 0,
                    pageWidth, 0,
                    pageWidth, pageHeight,
                    0, pageHeight,
                    0, 0 // close the rectangle
                };

                // Draw the polyline representing the border.
                long shapeId = page.DrawPolyline(borderPoints);

                // Retrieve the created shape.
                Shape borderShape = page.Shapes.GetShape(shapeId);

                // Set line pattern to dashed.
                borderShape.Line.LinePattern.Value = LinePatternValue.Dash;

                // Set line thickness (weight) – example: 0.02 inches.
                borderShape.Line.LineWeight.Value = 0.02;

                // Ensure no fill (transparent) – set fill pattern to 0 (none).
                borderShape.Fill.FillPattern.Value = 0;
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}