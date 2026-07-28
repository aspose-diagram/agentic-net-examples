using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output_resized.vsdx";

                // New page margins in inches
                double marginLeft = 0.5;   // 0.5 inch
                double marginRight = 0.5;  // 0.5 inch
                double marginTop = 0.5;    // 0.5 inch
                double marginBottom = 0.5; // 0.5 inch

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Work with the first page (adjust if needed)
                    Page page = diagram.Pages[0];

                    // Set new margins
                    page.PageSheet.PrintProps.PageLeftMargin.Value = marginLeft;
                    page.PageSheet.PrintProps.PageRightMargin.Value = marginRight;
                    page.PageSheet.PrintProps.PageTopMargin.Value = marginTop;
                    page.PageSheet.PrintProps.PageBottomMargin.Value = marginBottom;

                    // Page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Drawable area after applying margins
                    double drawableWidth = pageWidth - marginLeft - marginRight;
                    double drawableHeight = pageHeight - marginTop - marginBottom;

                    // Determine the bounding box of all shapes on the page
                    double minX = double.MaxValue;
                    double minY = double.MaxValue;
                    double maxX = double.MinValue;
                    double maxY = double.MinValue;

                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        double halfWidth = shape.XForm.Width.Value / 2.0;
                        double halfHeight = shape.XForm.Height.Value / 2.0;

                        double left = shape.XForm.PinX.Value - halfWidth;
                        double right = shape.XForm.PinX.Value + halfWidth;
                        double bottom = shape.XForm.PinY.Value - halfHeight;
                        double top = shape.XForm.PinY.Value + halfHeight;

                        if (left < minX) minX = left;
                        if (right > maxX) maxX = right;
                        if (bottom < minY) minY = bottom;
                        if (top > maxY) maxY = top;
                    }

                    // If there are no shapes, just save the diagram with new margins
                    if (minX == double.MaxValue || minY == double.MaxValue)
                    {
                        Console.WriteLine("No shapes found on the page. Saving diagram with updated margins only.");
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);
                        return;
                    }

                    double contentWidth = maxX - minX;
                    double contentHeight = maxY - minY;

                    // Compute uniform scaling factor to fit content within drawable area
                    double scaleX = drawableWidth / contentWidth;
                    double scaleY = drawableHeight / contentHeight;
                    double scale = Math.Min(scaleX, scaleY);

                    // Apply scaling and reposition shapes relative to the new margins
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Del == BOOL.True)
                            continue;

                        // Scale size
                        shape.XForm.Width.Value *= scale;
                        shape.XForm.Height.Value *= scale;

                        // Compute new center position
                        double offsetX = shape.XForm.PinX.Value - minX; // distance from left bound
                        double offsetY = shape.XForm.PinY.Value - minY; // distance from bottom bound

                        double newPinX = marginLeft + (offsetX * scale);
                        double newPinY = marginBottom + (offsetY * scale);

                        shape.XForm.PinX.Value = newPinX;
                        shape.XForm.PinY.Value = newPinY;
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved to '{outputPath}' with resized shapes.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }