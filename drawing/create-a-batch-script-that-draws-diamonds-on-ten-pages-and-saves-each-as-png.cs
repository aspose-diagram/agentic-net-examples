using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        using (Diagram diagram = new Diagram())
        {
            // Ensure the diagram has exactly 10 pages
            while (diagram.Pages.Count < 10)
            {
                diagram.Pages.Add(new Page());
            }

            // Loop through each page and draw a diamond shape
            for (int i = 0; i < 10; i++)
            {
                Page page = diagram.Pages[i];

                // Define diamond geometry (centered at (5,5) with width and height of 2 inches)
                double centerX = 5.0;
                double centerY = 5.0;
                double width = 2.0;
                double height = 2.0;
                double halfW = width / 2.0;
                double halfH = height / 2.0;

                // Points for the diamond (closed polyline)
                double[] points = new double[]
                {
                    centerX - halfW, centerY,          // left
                    centerX,          centerY + halfH, // top
                    centerX + halfW, centerY,          // right
                    centerX,          centerY - halfH, // bottom
                    centerX - halfW, centerY           // back to left to close
                };

                // Draw the diamond on the current page
                page.DrawPolyline(points);

                // Prepare PNG export options for the current page
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                saveOptions.PageIndex = i; // export only this page

                // Save the page as a PNG file
                string outputPath = $"Diamond_Page_{i + 1}.png";
                diagram.Save(outputPath, saveOptions);
            }
        }
    }
}
