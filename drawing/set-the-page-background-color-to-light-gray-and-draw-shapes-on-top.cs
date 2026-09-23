using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram())
            {
                // Get the default foreground page (first page in a new diagram)
                Page page = diagram.Pages[0];

                // Create a background page that will hold the page background color
                Page backgroundPage = new Page();
                backgroundPage.Background = BOOL.True; // mark as background page

                // Match background page size to the foreground page dimensions
                backgroundPage.PageSheet.PageProps.PageWidth.Value = page.PageSheet.PageProps.PageWidth.Value;
                backgroundPage.PageSheet.PageProps.PageHeight.Value = page.PageSheet.PageProps.PageHeight.Value;

                // Add the background page to the diagram's page collection
                diagram.Pages.Add(backgroundPage);

                // Add a rectangle that covers the entire background page (acts as background color)
                double bgWidth = backgroundPage.PageSheet.PageProps.PageWidth.Value;
                double bgHeight = backgroundPage.PageSheet.PageProps.PageHeight.Value;
                double bgPinX = bgWidth / 2.0;
                double bgPinY = bgHeight / 2.0;
                long bgShapeId = backgroundPage.DrawRectangle(bgPinX, bgPinY, bgWidth, bgHeight);
                Shape bgShape = backgroundPage.Shapes.GetShape(bgShapeId);
                bgShape.Fill.FillPattern.Value = 1;                     // solid fill pattern
                bgShape.Fill.FillForegnd.Value = "#D3D3D3";            // light gray fill color
                bgShape.Line.LinePattern.Value = LinePatternValue.None; // no border line
                bgShape.SendToBack();                                 // ensure background shape is behind others
                bgShape.Protection.LockSelect.Value = BOOL.True;     // make background shape non‑selectable

                // Link the foreground page to the newly created background page
                page.BackPage = backgroundPage;

                // Draw a red rectangle on the foreground page
                double rectPinX = bgWidth / 4.0;
                double rectPinY = bgHeight / 2.0;
                double rectWidth = bgWidth / 2.0;
                double rectHeight = bgHeight / 4.0;
                long rectId = page.DrawRectangle(rectPinX, rectPinY, rectWidth, rectHeight);
                Shape rectShape = page.Shapes.GetShape(rectId);
                rectShape.Fill.FillPattern.Value = 1;
                rectShape.Fill.FillForegnd.Value = "#FF0000";               // red fill
                rectShape.Line.LinePattern.Value = LinePatternValue.Solid; // solid border line
                rectShape.Line.LineColor.Value = "#000000";                // black border color

                // Draw a green ellipse on the foreground page
                double ellipsePinX = bgWidth * 3.0 / 4.0;
                double ellipsePinY = bgHeight / 2.0;
                double ellipseWidth = bgWidth / 3.0;
                double ellipseHeight = bgHeight / 3.0;
                long ellipseId = page.DrawEllipse(ellipsePinX, ellipsePinY, ellipseWidth, ellipseHeight);
                Shape ellipseShape = page.Shapes.GetShape(ellipseId);
                ellipseShape.Fill.FillPattern.Value = 1;
                ellipseShape.Fill.FillForegnd.Value = "#00FF00";               // green fill
                ellipseShape.Line.LinePattern.Value = LinePatternValue.Solid; // solid border line
                ellipseShape.Line.LineColor.Value = "#0000FF";                // blue border color

                // Save the diagram to VSDX format
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}