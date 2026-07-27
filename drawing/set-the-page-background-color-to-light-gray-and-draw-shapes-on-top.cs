using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path for the output diagram file
        string outputPath = "DiagramWithBackground.vsdx";

        try
        {
            // Create a new empty diagram inside a using block to ensure disposal
            using (Diagram diagram = new Diagram())
            {
                // Get the first (foreground) page (always exists in a new diagram)
                Page foregroundPage = diagram.Pages[0];

                // Determine page dimensions (in inches) for later positioning
                double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

                // -------------------------------------------------
                // Create a background page and set it as a background
                // -------------------------------------------------
                // Find the maximum existing page ID to assign a new unique ID
                int maxPageId = 0;
                foreach (Page p in diagram.Pages)
                {
                    if (p.ID > maxPageId)
                        maxPageId = p.ID;
                }

                // Initialise a new background page
                Page backgroundPage = new Page();
                backgroundPage.ID = maxPageId + 1;
                backgroundPage.Name = "Background";
                backgroundPage.Background = BOOL.True; // Mark as background page

                // Add the background page to the diagram
                diagram.Pages.Add(backgroundPage);

                // Add a rectangle that covers the entire page on the background page
                // PinX/Y are the centre of the shape
                double bgRectPinX = pageWidth / 2.0;
                double bgRectPinY = pageHeight / 2.0;
                long bgRectId = backgroundPage.AddShape(bgRectPinX, bgRectPinY, pageWidth, pageHeight, "Rectangle");
                Shape bgRect = backgroundPage.Shapes.GetShape(bgRectId);

                // Set fill to light gray and remove outline (no line)
                bgRect.Fill.FillPattern.Value = 1;               // Solid fill
                bgRect.Fill.FillForegnd.Value = "#D3D3D3";       // Light gray
                bgRect.Line.LinePattern.Value = (LinePatternValue)0; // No line (enum cast)
                bgRect.SendToBack();                            // Ensure it stays behind other shapes

                // Link the background page to the foreground page
                foregroundPage.BackPage = backgroundPage;

                // -------------------------------------------------
                // Draw shapes on the foreground page (on top of background)
                // -------------------------------------------------

                // 1. Blue rectangle
                long rectId = foregroundPage.DrawRectangle(2.0, 2.0, 4.0, 2.0);
                Shape rectShape = foregroundPage.Shapes.GetShape(rectId);
                rectShape.Fill.FillPattern.Value = 1;
                rectShape.Fill.FillForegnd.Value = "#ADD8E6"; // Light blue
                rectShape.Line.LinePattern.Value = LinePatternValue.Solid; // Solid line
                rectShape.Line.LineColor.Value = "#0000FF"; // Blue line

                // 2. Red ellipse
                long ellipseId = foregroundPage.DrawEllipse(6.0, 5.0, 3.0, 2.0);
                Shape ellipseShape = foregroundPage.Shapes.GetShape(ellipseId);
                ellipseShape.Fill.FillPattern.Value = 1;
                ellipseShape.Fill.FillForegnd.Value = "#FFC0CB"; // Light pink (redish)
                ellipseShape.Line.LinePattern.Value = LinePatternValue.Solid;
                ellipseShape.Line.LineColor.Value = "#FF0000"; // Red line

                // 3. Green polyline (triangle)
                double[] points = new double[] { 5.0, 1.0, 7.0, 3.0, 3.0, 3.0, 5.0, 1.0 }; // Close the shape
                long polyId = foregroundPage.DrawPolyline(0, 0, 0, 0, points);
                Shape polyShape = foregroundPage.Shapes.GetShape(polyId);
                polyShape.Fill.FillPattern.Value = 1;
                polyShape.Fill.FillForegnd.Value = "#90EE90"; // Light green
                polyShape.Line.LinePattern.Value = LinePatternValue.Solid;
                polyShape.Line.LineColor.Value = "#008000"; // Dark green line

                // -------------------------------------------------
                // Save the diagram to a VSDX file
                // -------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}