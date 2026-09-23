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

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define which pages to apply the border to (by name)
            string[] targetPageNames = { "Page-1", "Page-2" };

            // Border settings
            string borderColor = "#000000";          // Black color
            double borderThicknessInInches = 0.02;   // Approx. 0.5 mm
            LinePatternValue dashPattern = LinePatternValue.Dash; // Dashed line

            foreach (Page page in diagram.Pages)
            {
                // Check if the current page is one of the selected pages
                bool isTarget = false;
                foreach (string name in targetPageNames)
                {
                    if (page.Name == name)
                    {
                        isTarget = true;
                        break;
                    }
                }

                if (!isTarget)
                    continue;

                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate the center point of the page (PinX, PinY)
                double centerX = pageWidth / 2.0;
                double centerY = pageHeight / 2.0;

                // Draw a rectangle that matches the page size
                long rectShapeId = page.DrawRectangle(centerX, centerY, pageWidth, pageHeight);

                // Retrieve the shape object to set its line style
                Shape borderShape = page.Shapes.GetShape(rectShapeId);

                // Set line color, pattern (dashed), and thickness
                borderShape.Line.LineColor.Value = borderColor;
                borderShape.Line.LinePattern.Value = dashPattern;
                borderShape.Line.LineWeight.Value = borderThicknessInInches;

                // Optional: make the rectangle transparent (no fill)
                borderShape.Fill.FillPattern.Value = 0; // No fill
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up
            diagram.Dispose();

            Console.WriteLine("Page borders applied and diagram saved to " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
