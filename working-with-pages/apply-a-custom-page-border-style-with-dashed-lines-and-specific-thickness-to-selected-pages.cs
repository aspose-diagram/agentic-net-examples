using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define which pages to apply the border to (zero‑based indices)
                List<int> pagesToBorder = new List<int> { 0, 2 }; // example: first and third pages

                // Border settings
                double marginInches = 0.1;               // inset from page edges
                double lineThicknessInches = 0.02;       // line weight
                string lineColorHex = "#0000FF";        // blue dashed line

                foreach (int pageIndex in pagesToBorder)
                {
                    // Ensure the page index is valid
                    if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
                    {
                        Console.WriteLine($"Page index {pageIndex} is out of range.");
                        continue;
                    }

                    // Retrieve the page
                    Page page = diagram.Pages[pageIndex];

                    // Get page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Calculate rectangle size and position (centered)
                    double rectWidth = pageWidth - 2 * marginInches;
                    double rectHeight = pageHeight - 2 * marginInches;
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Draw the rectangle that will serve as the page border
                    long borderShapeId = page.DrawRectangle(pinX, pinY, rectWidth, rectHeight);

                    // Retrieve the shape object
                    Shape borderShape = page.Shapes.GetShape(borderShapeId);

                    // Set line style to dashed
                    borderShape.Line.LinePattern.Value = LinePatternValue.Dash;

                    // Set line thickness
                    borderShape.Line.LineWeight.Value = lineThicknessInches;

                    // Set line color
                    borderShape.Line.LineColor.Value = lineColorHex;

                    // Remove any fill (transparent)
                    borderShape.Fill.FillPattern.Value = 0; // No fill
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up
                diagram.Dispose();

                Console.WriteLine("Page borders applied and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }