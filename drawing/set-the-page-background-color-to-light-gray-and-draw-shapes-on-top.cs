using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            using (Diagram diagram = new Diagram())
            {
                // -----------------------------------------------------------------
                // 1. Create the foreground page (default page)
                // -----------------------------------------------------------------
                diagram.Pages.Add(new Page());                     // Add a new page
                Page foregroundPage = diagram.Pages[0];            // Retrieve the first page

                // Set page size (8.5 x 11 inches)
                foregroundPage.PageSheet.PageProps.PageWidth.Value = 8.5;
                foregroundPage.PageSheet.PageProps.PageHeight.Value = 11.0;

                // -----------------------------------------------------------------
                // 2. Create a background page and set it as a background
                // -----------------------------------------------------------------
                Page backgroundPage = new Page();
                backgroundPage.Background = BOOL.True;             // Mark as background page
                backgroundPage.PageSheet.PageProps.PageWidth.Value = 8.5;
                backgroundPage.PageSheet.PageProps.PageHeight.Value = 11.0;
                diagram.Pages.Add(backgroundPage);                 // Add background page to diagram

                // Link the foreground page to the background page
                foregroundPage.BackPage = backgroundPage;

                // -----------------------------------------------------------------
                // 3. Add a rectangle covering the whole page on the background page
                // -----------------------------------------------------------------
                long bgRectId = backgroundPage.DrawRectangle(0, 0, 8.5, 11.0);
                Shape bgRect = backgroundPage.Shapes.GetShape(bgRectId);

                // Fill with light gray (#D3D3D3) and remove outline
                bgRect.Fill.FillPattern.Value = 1;                 // Solid fill
                bgRect.Fill.FillForegnd.Value = "#D3D3D3";         // Light gray color
                bgRect.Line.LinePattern.Value = LinePatternValue.Solid;
                bgRect.Line.LineWeight.Value = 0.0;                // No visible line

                // Send to back and lock selection so it behaves as a true background
                bgRect.SendToBack();
                bgRect.Protection.LockSelect.Value = BOOL.True;

                // -----------------------------------------------------------------
                // 4. Draw shapes on the foreground page (on top of the background)
                // -----------------------------------------------------------------
                // Example: Red ellipse
                long ellipseId = foregroundPage.DrawEllipse(4.0, 5.5, 2.0, 1.0);
                Shape ellipse = foregroundPage.Shapes.GetShape(ellipseId);
                ellipse.Fill.FillPattern.Value = 1;
                ellipse.Fill.FillForegnd.Value = "#FF0000";        // Red color
                ellipse.Line.LinePattern.Value = LinePatternValue.Solid;
                ellipse.Line.LineWeight.Value = 0.02;

                // Example: Blue rectangle
                long rectId = foregroundPage.DrawRectangle(2.0, 2.0, 3.0, 2.0);
                Shape rect = foregroundPage.Shapes.GetShape(rectId);
                rect.Fill.FillPattern.Value = 1;
                rect.Fill.FillForegnd.Value = "#0000FF";           // Blue color
                rect.Line.LinePattern.Value = LinePatternValue.Solid;
                rect.Line.LineWeight.Value = 0.02;

                // Save the diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}