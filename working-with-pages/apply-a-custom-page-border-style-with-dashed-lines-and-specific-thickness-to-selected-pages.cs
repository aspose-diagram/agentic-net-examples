using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output Visio file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Define which pages to apply the border to (zero‑based indices)
                int[] selectedPageIndices = { 0, 2 }; // example: first and third pages

                // Border style settings
                double borderThicknessInInches = 0.02; // approx 0.5 pt
                string borderColorHex = "#000000";    // black
                LinePatternValue borderPattern = LinePatternValue.Dash;

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    foreach (int pageIndex in selectedPageIndices)
                    {
                        // Ensure the page index is valid
                        if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
                            continue;

                        Page page = diagram.Pages[pageIndex];

                        // Retrieve page dimensions (in inches)
                        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Calculate the center point of the page
                        double centerX = pageWidth / 2.0;
                        double centerY = pageHeight / 2.0;

                        // Draw a rectangle that matches the page size
                        long rectShapeId = page.DrawRectangle(centerX, centerY, pageWidth, pageHeight);

                        // Retrieve the newly created shape
                        Shape borderShape = page.Shapes.GetShape(rectShapeId);

                        // Apply line styling
                        borderShape.Line.LineColor.Value = borderColorHex;
                        borderShape.Line.LinePattern.Value = borderPattern;
                        borderShape.Line.LineWeight.Value = borderThicknessInInches;

                        // Ensure the rectangle has no fill
                        borderShape.Fill.FillPattern.Value = 0; // No fill
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }