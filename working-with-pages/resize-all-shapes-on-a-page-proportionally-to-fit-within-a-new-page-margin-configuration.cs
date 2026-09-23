using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Define new page margins (in inches)
                double marginLeft = 0.5;
                double marginRight = 0.5;
                double marginTop = 0.5;
                double marginBottom = 0.5;

                // Process each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Set the new margins on the page
                    page.PageSheet.PrintProps.PageLeftMargin.Value = marginLeft;
                    page.PageSheet.PrintProps.PageRightMargin.Value = marginRight;
                    page.PageSheet.PrintProps.PageTopMargin.Value = marginTop;
                    page.PageSheet.PrintProps.PageBottomMargin.Value = marginBottom;

                    // Get page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Compute the usable drawing area after applying margins
                    double usableWidth = pageWidth - marginLeft - marginRight;
                    double usableHeight = pageHeight - marginTop - marginBottom;

                    // Determine the bounding box of all non‑deleted shapes on the page
                    double minX = double.MaxValue;
                    double maxX = double.MinValue;
                    double minY = double.MaxValue;
                    double maxY = double.MinValue;

                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
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

                    // If no shapes were found, skip scaling for this page
                    if (minX == double.MaxValue)
                        continue;

                    double drawingWidth = maxX - minX;
                    double drawingHeight = maxY - minY;

                    // Compute uniform scale factor to fit within usable area
                    double scaleX = usableWidth / drawingWidth;
                    double scaleY = usableHeight / drawingHeight;
                    double scale = Math.Min(scaleX, scaleY);

                    // Apply scaling and reposition each shape
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Del == BOOL.True)
                            continue;

                        // Original dimensions
                        double origWidth = shape.XForm.Width.Value;
                        double origHeight = shape.XForm.Height.Value;
                        double origPinX = shape.XForm.PinX.Value;
                        double origPinY = shape.XForm.PinY.Value;

                        // Compute original extents
                        double origLeft = origPinX - origWidth / 2.0;
                        double origBottom = origPinY - origHeight / 2.0;

                        // Scale dimensions
                        double newWidth = origWidth * scale;
                        double newHeight = origHeight * scale;

                        // Scale position relative to the original bounding box
                        double newLeft = marginLeft + (origLeft - minX) * scale;
                        double newBottom = marginBottom + (origBottom - minY) * scale;

                        // Set new transformed values
                        shape.XForm.Width.Value = newWidth;
                        shape.XForm.Height.Value = newHeight;
                        shape.XForm.PinX.Value = newLeft + newWidth / 2.0;
                        shape.XForm.PinY.Value = newBottom + newHeight / 2.0;
                    }
                }

                // Save the modified diagram
                string outputPath = "output_resized.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }