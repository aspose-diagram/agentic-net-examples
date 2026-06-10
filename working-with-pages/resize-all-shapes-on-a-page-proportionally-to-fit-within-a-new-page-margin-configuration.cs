using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = "output_resized.vsdx";

        try
        {
            Diagram diagram = new Diagram(inputPath);
            Page page = diagram.Pages[0];

            // New margin configuration (in inches)
            double marginLeft = 0.5;
            double marginRight = 0.5;
            double marginTop = 0.5;
            double marginBottom = 0.5;

            // Page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Drawable area after applying margins
            double drawableWidth = pageWidth - marginLeft - marginRight;
            double drawableHeight = pageHeight - marginTop - marginBottom;

            // Determine the bounding box of all non‑deleted shapes
            double minX = double.MaxValue;
            double minY = double.MaxValue;
            double maxX = double.MinValue;
            double maxY = double.MinValue;

            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.False)
                {
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
            }

            // Content size
            double contentWidth = maxX - minX;
            double contentHeight = maxY - minY;

            // Guard against empty or zero‑size content
            if (contentWidth <= 0 || contentHeight <= 0)
            {
                Console.WriteLine("No shapes to resize or invalid content size.");
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                return;
            }

            // Scale factor to fit content within the drawable area
            double scaleX = drawableWidth / contentWidth;
            double scaleY = drawableHeight / contentHeight;
            double scale = Math.Min(scaleX, scaleY);

            // Apply scaling and translation to each shape
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.False)
                {
                    // Scale size
                    shape.XForm.Width.Value *= scale;
                    shape.XForm.Height.Value *= scale;

                    // Compute new position relative to the bounding box origin
                    double relX = shape.XForm.PinX.Value - minX;
                    double relY = shape.XForm.PinY.Value - minY;

                    // Apply scaling and margins
                    shape.XForm.PinX.Value = marginLeft + relX * scale;
                    shape.XForm.PinY.Value = marginBottom + relY * scale;
                }
            }

            // Save the resized diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}