using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths (replace with actual paths)
                string inputPath = "input.vsdx";
                string outputPath = "output_fitted.vsdx";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Process each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        double minX = double.MaxValue;
                        double maxX = double.MinValue;

                        // Determine the horizontal extents of all non‑deleted shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted
                            if (shape.Del == BOOL.True)
                                continue;

                            // Calculate left and right bounds of the shape
                            double left = shape.XForm.PinX.Value - shape.XForm.Width.Value / 2.0;
                            double right = shape.XForm.PinX.Value + shape.XForm.Width.Value / 2.0;

                            if (left < minX) minX = left;
                            if (right > maxX) maxX = right;
                        }

                        // If no shapes were found, skip resizing for this page
                        if (minX == double.MaxValue || maxX == double.MinValue)
                            continue;

                        // Shift all shapes left so that the leftmost shape aligns with the page's left edge (0)
                        double shift = -minX;
                        if (Math.Abs(shift) > 0.0001)
                        {
                            foreach (Shape shape in page.Shapes)
                            {
                                if (shape.Del == BOOL.True)
                                    continue;

                                shape.XForm.PinX.Value += shift;
                            }
                        }

                        // Set the page width to tightly fit the content
                        double newPageWidth = maxX - minX;
                        if (newPageWidth < 0) newPageWidth = 0; // safeguard
                        page.PageSheet.PageProps.PageWidth.Value = newPageWidth;
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Diagram processing completed. Saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }