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

            // Load an existing Visio diagram (replace with your file path)
            string inputPath = "input.vsdx";
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create a single stylesheet that defines a white fill
                StyleSheet whiteStyle = new StyleSheet();
                whiteStyle.ID = diagram.StyleSheets.Count + 1;
                whiteStyle.Fill.FillForegnd.Value = "#FFFFFF"; // white color
                whiteStyle.Fill.FillPattern.Value = 1; // solid fill
                diagram.StyleSheets.Add(whiteStyle);

                // Iterate through all pages and add a full‑page white background shape
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center coordinates for the rectangle
                    double centerX = pageWidth / 2.0;
                    double centerY = pageHeight / 2.0;

                    // Draw a rectangle that spans the entire page
                    long bgShapeId = page.DrawRectangle(centerX, centerY, pageWidth, pageHeight);
                    Shape bgShape = page.Shapes.GetShape(bgShapeId);

                    // Apply the white fill from the stylesheet
                    bgShape.Fill.FillForegnd.Value = whiteStyle.Fill.FillForegnd.Value;
                    bgShape.Fill.FillPattern.Value = whiteStyle.Fill.FillPattern.Value;

                    // Remove any border
                    bgShape.Line.LinePattern.Value = LinePatternValue.None;
                    bgShape.Line.LineWeight.Value = 0.0;

                    // Send the shape to the back so it appears as a background
                    bgShape.SendToBack();

                    // Make the background shape non‑selectable
                    bgShape.Protection.LockSelect.Value = BOOL.True;
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
