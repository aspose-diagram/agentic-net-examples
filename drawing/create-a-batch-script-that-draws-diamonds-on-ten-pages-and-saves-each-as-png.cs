using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Create a new blank diagram (contains one default page)
        using (Diagram diagram = new Diagram())
        {
            // Loop to create ten pages and draw a diamond on each
            for (int i = 0; i < 10; i++)
            {
                Page page;

                // Use the existing first page for i == 0, otherwise add a new page
                if (i == 0)
                {
                    page = diagram.Pages[0];
                }
                else
                {
                    Page newPage = new Page();
                    diagram.Pages.Add(newPage);
                    page = newPage;
                }

                // Determine page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center of the page
                double centerX = pageWidth / 2.0;
                double centerY = pageHeight / 2.0;

                // Size of the diamond (distance from center to each vertex)
                double halfSize = 2.0; // inches

                // Define the diamond vertices and close the shape by repeating the first point
                double[] diamondPoints = new double[]
                {
                    centerX,               centerY - halfSize, // Top
                    centerX + halfSize,    centerY,            // Right
                    centerX,               centerY + halfSize, // Bottom
                    centerX - halfSize,    centerY,            // Left
                    centerX,               centerY - halfSize  // Close back to Top
                };

                // Draw the diamond (returns a shape ID)
                long shapeId = page.DrawPolyline(diamondPoints);

                // Retrieve the shape to apply styling
                Shape diamondShape = page.Shapes.GetShape((int)shapeId);
                diamondShape.Line.LineColor.Value = "#FF0000";      // Red border
                diamondShape.Fill.FillForegnd.Value = "#00FF00";    // Green fill

                // Save the current page as a PNG file
                string outputFile = $"DiamondPage_{i + 1}.png";
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                pngOptions.PageIndex = i; // Export only this page
                diagram.Save(outputFile, pngOptions);
            }
        }
    }
}
