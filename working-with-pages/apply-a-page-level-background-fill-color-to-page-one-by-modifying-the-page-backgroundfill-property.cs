using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Create a new empty diagram
        using (Diagram diagram = new Diagram())
        {
            try
            {
                // Ensure at least one foreground page exists
                if (diagram.Pages.Count == 0)
                {
                    diagram.Pages.Add(new Page());
                }

                // Retrieve the first (foreground) page
                Page foregroundPage = diagram.Pages[0];

                // Create a new background page
                Page backgroundPage = new Page();

                // Mark the page as a background canvas
                backgroundPage.Background = BOOL.True;

                // Add the background page to the diagram
                diagram.Pages.Add(backgroundPage);

                // Obtain the dimensions of the foreground page (in inches)
                double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

                // Calculate the centre point for the rectangle shape
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Add a rectangle shape that spans the whole page on the background page
                // The last argument (isCalculate) must be a bool, not an int
                long rectId = backgroundPage.AddShape(pinX, pinY, pageWidth, pageHeight, "Rectangle", false);

                // Retrieve the newly added shape to set its fill properties
                Shape backgroundRect = backgroundPage.Shapes.GetShape(rectId);

                // Set a solid fill pattern (1) and a light‑blue colour
                backgroundRect.Fill.FillPattern.Value = 1;               // 1 = solid fill
                backgroundRect.Fill.FillForegnd.Value = "#ADD8E6";       // Light blue

                // Remove the border by setting the line pattern to 0 (no line)
                backgroundRect.Line.LinePattern.Value = 0;

                // Link the foreground page to the background page
                foregroundPage.BackPage = backgroundPage;

                // Save the diagram as VSDX
                diagram.Save("PageBackground.vsdx", SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                // Output any errors that occur during processing
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}