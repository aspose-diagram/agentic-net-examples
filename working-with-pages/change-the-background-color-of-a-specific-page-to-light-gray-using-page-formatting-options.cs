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

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the target page (e.g., the first page)
            Page targetPage = diagram.Pages[0];

            // Get page dimensions (in inches)
            double pageWidth = targetPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = targetPage.PageSheet.PageProps.PageHeight.Value;

            // Create a new background page
            Page backgroundPage = new Page();
            backgroundPage.Background = BOOL.True; // Mark as a background page

            // Calculate the center position for the rectangle that will cover the whole page
            double pinX = pageWidth / 2.0;
            double pinY = pageHeight / 2.0;

            // Add a rectangle shape that spans the entire page area
            long rectId = backgroundPage.AddShape(pinX, pinY, pageWidth, pageHeight, "Rectangle");

            // Retrieve the rectangle shape to apply formatting
            Shape rectShape = backgroundPage.Shapes.GetShape(rectId);

            // Set solid fill pattern
            rectShape.Fill.FillPattern.Value = 1;               // Solid fill
            rectShape.Fill.FillForegnd.Value = "#D3D3D3";       // Light gray color

            // Remove border by setting line pattern to none
            rectShape.Line.LinePattern.Value = (LinePatternValue)0; // No line

            // Send the rectangle to the back so other shapes appear above it
            rectShape.SendToBack();

            // Optionally lock the background shape to prevent selection
            rectShape.Protection.LockSelect.Value = BOOL.True;

            // Add the background page to the diagram
            diagram.Pages.Add(backgroundPage);

            // Associate the background page with the target page
            targetPage.BackPage = backgroundPage;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
