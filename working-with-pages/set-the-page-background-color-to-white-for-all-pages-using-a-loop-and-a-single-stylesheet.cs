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

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create a single stylesheet for white background fill
                StyleSheet whiteStyle = new StyleSheet();
                whiteStyle.ID = diagram.StyleSheets.Count + 1;
                whiteStyle.Name = "WhiteBackground";
                whiteStyle.Fill.FillForegnd.Value = "#FFFFFF";   // White fill color
                whiteStyle.Fill.FillPattern.Value = 1;          // Solid pattern
                diagram.StyleSheets.Add(whiteStyle);

                // Determine the current maximum page ID to assign unique IDs to new background pages
                int maxPageId = 0;
                foreach (Page p in diagram.Pages)
                {
                    if (p.ID > maxPageId)
                        maxPageId = p.ID;
                }

                // Loop through all existing pages
                foreach (Page page in diagram.Pages)
                {
                    // Create a new background page
                    Page bgPage = new Page();
                    bgPage.ID = ++maxPageId;
                    bgPage.Name = $"Background_{page.ID}";
                    bgPage.Background = BOOL.True; // Mark as background page

                    // Add the background page to the diagram
                    diagram.Pages.Add(bgPage);

                    // Link the foreground page to its background page
                    page.BackPage = bgPage;

                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Add a rectangle shape that spans the entire page
                    // PinX and PinY are the center of the shape
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;
                    long shapeId = bgPage.AddShape(pinX, pinY, pageWidth, pageHeight, "Rectangle");

                    // Retrieve the shape object
                    Shape bgShape = bgPage.Shapes.GetShape(shapeId);

                    // Apply the white background stylesheet
                    bgShape.FillStyle = whiteStyle;

                    // Ensure the shape has no visible border
                    bgShape.Line.LinePattern.Value = 0;          // No line pattern
                    bgShape.Line.LineColor.Value = "#FFFFFF";    // Border color (irrelevant when pattern is 0)

                    // Send the shape to the back so other content appears above it
                    bgPage.SendToBack(shapeId);
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
