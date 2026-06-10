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
            // Path to the output Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Index of the page whose background we want to change (0‑based)
                int targetPageIndex = 0;

                // Retrieve the target page
                Page targetPage = diagram.Pages[targetPageIndex];

                // Determine a new unique page ID
                int maxPageId = 0;
                foreach (Page p in diagram.Pages)
                {
                    if (p.ID > maxPageId)
                        maxPageId = p.ID;
                }

                // Create a new background page
                Page backgroundPage = new Page(maxPageId + 1);
                backgroundPage.Name = "BackgroundPage";
                backgroundPage.Background = BOOL.True; // Mark as a background page

                // Copy the dimensions of the target page
                double pageWidth = targetPage.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = targetPage.PageSheet.PageProps.PageHeight.Value;
                backgroundPage.PageSheet.PageProps.PageWidth.Value = pageWidth;
                backgroundPage.PageSheet.PageProps.PageHeight.Value = pageHeight;

                // Draw a rectangle that covers the entire page
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;
                long rectShapeId = backgroundPage.DrawRectangle(pinX, pinY, pageWidth, pageHeight);
                Shape rectShape = backgroundPage.Shapes.GetShape((int)rectShapeId);

                // Set the rectangle fill to solid light gray (#D3D3D3)
                rectShape.Fill.FillPattern.Value = 1;               // Solid fill
                rectShape.Fill.FillForegnd.Value = "#D3D3D3";       // Light gray color
                rectShape.Line.LinePattern.Value = 0;               // No border

                // Send the rectangle to the back so other shapes appear above it
                backgroundPage.SendToBack(rectShapeId);

                // Add the background page to the diagram
                diagram.Pages.Add(backgroundPage);

                // Associate the background page with the target page
                targetPage.BackPage = backgroundPage;

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
