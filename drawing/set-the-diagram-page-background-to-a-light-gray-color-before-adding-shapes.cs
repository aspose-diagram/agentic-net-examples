using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // -------------------------------------------------
            // Create the foreground page (the main drawing page)
            // -------------------------------------------------
            Page foregroundPage = new Page();
            diagram.Pages.Add(foregroundPage);

            // -------------------------------------------------
            // Create a background page that will hold the background color
            // -------------------------------------------------
            Page backgroundPage = new Page();
            backgroundPage.Background = BOOL.True; // Mark as a background page
            diagram.Pages.Add(backgroundPage);

            // Retrieve page dimensions from the foreground page
            double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

            // -------------------------------------------------
            // Add a rectangle shape that spans the entire page on the background page
            // -------------------------------------------------
            long bgShapeId = backgroundPage.AddShape(0, 0, pageWidth, pageHeight, "Rectangle", false);
            Shape bgShape = backgroundPage.Shapes.GetShape(bgShapeId);

            // Set the shape to a solid fill with light gray color (#ADD8E6)
            bgShape.Fill.FillPattern.Value = 1;               // Solid fill
            bgShape.Fill.FillBkgnd.Value = "#ADD8E6";         // Light gray background color

            // Remove any outline stroke
            bgShape.Line.LinePattern.Value = 0;               // No line pattern (invisible)

            // Send the background shape to the back and lock it from selection
            bgShape.SendToBack();
            bgShape.Protection.LockSelect.Value = BOOL.True;

            // Link the foreground page to the background page
            foregroundPage.BackPage = backgroundPage;

            // -------------------------------------------------
            // Example: add a regular shape on the foreground page
            // -------------------------------------------------
            long shapeId = foregroundPage.AddShape(2, 2, 1, 1, "Rectangle", false);
            Shape shape = foregroundPage.Shapes.GetShape(shapeId);
            shape.Text.Value.Add(new Txt("Sample Shape"));

            // -------------------------------------------------
            // Save the diagram
            // -------------------------------------------------
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
