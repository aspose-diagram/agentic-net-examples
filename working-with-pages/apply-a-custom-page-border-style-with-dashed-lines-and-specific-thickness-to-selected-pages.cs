using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                // Replace "input.vsdx" with the path to your source file
                Diagram diagram = new Diagram("input.vsdx");

                // Define border line style
                const double lineThicknessInInches = 0.02; // Example thickness
                const string lineColorHex = "#000000";    // Black color

                // Apply border to each page (or add your own selection logic)
                foreach (Page page in diagram.Pages)
                {
                    // Get page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Define a small margin so the border is fully visible within the page
                    double margin = 0.1; // inches

                    // Calculate rectangle coordinates
                    double left = margin;
                    double bottom = margin;
                    double right = pageWidth - margin;
                    double top = pageHeight - margin;

                    // Draw rectangle representing the page border
                    // DrawRectangle(pinX, pinY, width, height)
                    // pinX/Y are the center of the rectangle
                    double rectCenterX = (left + right) / 2.0;
                    double rectCenterY = (bottom + top) / 2.0;
                    double rectWidth = right - left;
                    double rectHeight = top - bottom;

                    long borderShapeId = page.DrawRectangle(rectCenterX, rectCenterY, rectWidth, rectHeight);
                    Shape borderShape = page.Shapes.GetShape(borderShapeId);

                    // Set line pattern to dashed
                    borderShape.Line.LinePattern.Value = LinePatternValue.Dash;

                    // Set line thickness
                    borderShape.Line.LineWeight.Value = lineThicknessInInches;

                    // Set line color
                    borderShape.Line.LineColor.Value = lineColorHex;

                    // Ensure the shape has no fill (transparent)
                    borderShape.Fill.FillPattern.Value = 0; // No fill
                }

                // Save the modified diagram
                // Replace "output.vsdx" with the desired output path
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

                // Dispose diagram to release resources
                diagram.Dispose();

                Console.WriteLine("Page borders applied and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }