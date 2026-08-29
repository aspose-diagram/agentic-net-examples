using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Process each page separately
            foreach (Page page in diagram.Pages)
            {
                // Collect non‑deleted, non‑connector shapes for processing
                var shapeIds = new System.Collections.Generic.List<long>();
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True) continue;
                    // Skip 1‑D connector shapes
                    if (shape.OneD) continue;
                    shapeIds.Add(shape.ID);
                }

                // Simple collision resolution: shift overlapping shapes to the right
                const double margin = 0.5; // extra space in inches between shapes
                const double step = 0.5;   // incremental move step in inches

                // Iterate over shapes in the order they were added
                for (int i = 0; i < shapeIds.Count; i++)
                {
                    Shape shapeI = page.Shapes.GetShape(shapeIds[i]);

                    // Compute bounding box for shapeI
                    double iLeft = shapeI.XForm.PinX.Value - shapeI.XForm.Width.Value / 2.0;
                    double iRight = shapeI.XForm.PinX.Value + shapeI.XForm.Width.Value / 2.0;
                    double iTop = shapeI.XForm.PinY.Value + shapeI.XForm.Height.Value / 2.0;
                    double iBottom = shapeI.XForm.PinY.Value - shapeI.XForm.Height.Value / 2.0;

                    bool moved;
                    do
                    {
                        moved = false;
                        // Compare with all previously positioned shapes
                        for (int j = 0; j < i; j++)
                        {
                            Shape shapeJ = page.Shapes.GetShape(shapeIds[j]);

                            // Compute bounding box for shapeJ
                            double jLeft = shapeJ.XForm.PinX.Value - shapeJ.XForm.Width.Value / 2.0;
                            double jRight = shapeJ.XForm.PinX.Value + shapeJ.XForm.Width.Value / 2.0;
                            double jTop = shapeJ.XForm.PinY.Value + shapeJ.XForm.Height.Value / 2.0;
                            double jBottom = shapeJ.XForm.PinY.Value - shapeJ.XForm.Height.Value / 2.0;

                            // Check for rectangle intersection
                            bool overlapX = iLeft < jRight && iRight > jLeft;
                            bool overlapY = iBottom < jTop && iTop > jBottom;
                            if (overlapX && overlapY)
                            {
                                // Overlap detected – shift shapeI to the right
                                shapeI.XForm.PinX.Value += step;
                                // Re‑calculate bounding box after move
                                iLeft = shapeI.XForm.PinX.Value - shapeI.XForm.Width.Value / 2.0;
                                iRight = shapeI.XForm.PinX.Value + shapeI.XForm.Width.Value / 2.0;
                                // Mark that we moved and need to re‑check against earlier shapes
                                moved = true;
                                break;
                            }
                        }
                    } while (moved);
                }
            }

            // Save the adjusted diagram to the output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved without overlaps to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}