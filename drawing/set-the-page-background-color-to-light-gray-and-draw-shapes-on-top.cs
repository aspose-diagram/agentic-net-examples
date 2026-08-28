using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for SaveFileFormat enum

class Program
{
    static void Main(string[] args)
    {
        // Wrap all Aspose operations in a try/catch block to handle potential errors
        try
        {
            // Create a new empty Visio diagram
            using (Diagram diagram = new Diagram())
            {
                // Get the first (foreground) page
                Page foregroundPage = diagram.Pages[0];

                // Retrieve page dimensions (in inches) for later use
                double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

                // -------------------------------------------------
                // Create a background page and set its background flag
                // -------------------------------------------------
                Page backgroundPage = new Page();
                backgroundPage.Background = BOOL.True; // Mark page as a background page
                diagram.Pages.Add(backgroundPage); // Add background page to the diagram

                // Link the foreground page to the newly created background page
                foregroundPage.BackPage = backgroundPage;

                // Add a rectangle shape that spans the entire page size on the background page
                // PinX and PinY represent the shape's center point
                long bgShapeId = backgroundPage.AddShape(pageWidth / 2.0, pageHeight / 2.0, "Rectangle");
                Shape bgShape = backgroundPage.Shapes.GetShape(bgShapeId);

                // Set the rectangle size to match the page dimensions
                bgShape.XForm.Width.Value = pageWidth;
                bgShape.XForm.Height.Value = pageHeight;

                // Apply a solid light gray fill (#D3D3D3) and remove the border line
                bgShape.Fill.FillPattern.Value = 1;               // Solid fill pattern
                bgShape.Fill.FillForegnd.Value = "#D3D3D3";       // Light gray color
                bgShape.Line.LinePattern.Value = LinePatternValue.None; // No border line

                // Send the background shape to the back so other shapes appear on top
                bgShape.SendToBack();

                // -------------------------------------------------
                // Draw some sample shapes on the foreground page
                // -------------------------------------------------

                // 1. Draw a red rectangle
                long rectId = foregroundPage.DrawRectangle(1.0, 1.0, 2.0, 1.5);
                Shape rectShape = foregroundPage.Shapes.GetShape(rectId);
                rectShape.Fill.FillPattern.Value = 1;
                rectShape.Fill.FillForegnd.Value = "#FF0000"; // Red fill
                rectShape.Line.LinePattern.Value = LinePatternValue.Solid; // Solid border line

                // 2. Draw a blue ellipse
                long ellipseId = foregroundPage.DrawEllipse(4.0, 2.0, 1.5, 1.0);
                Shape ellipseShape = foregroundPage.Shapes.GetShape(ellipseId);
                ellipseShape.Fill.FillPattern.Value = 1;
                ellipseShape.Fill.FillForegnd.Value = "#0000FF"; // Blue fill
                ellipseShape.Line.LinePattern.Value = LinePatternValue.Solid; // Solid border line

                // 3. Draw a polyline (triangle) using a flat double array
                long polylineId = foregroundPage.DrawPolyline(6.0, 1.0, 7.0, 3.0, new double[] { 5.0, 3.0 });
                Shape polylineShape = foregroundPage.Shapes.GetShape(polylineId);
                polylineShape.Line.LinePattern.Value = LinePatternValue.Solid; // Solid line
                polylineShape.Line.LineColor.Value = "#008000"; // Green line color

                // -------------------------------------------------
                // Save the diagram to a VSDX file
                // -------------------------------------------------
                string outputPath = "DiagramWithBackground.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any exception details to the error stream and exit
            Console.Error.WriteLine($"Error: {ex.Message}");
            return;
        }
    }
}