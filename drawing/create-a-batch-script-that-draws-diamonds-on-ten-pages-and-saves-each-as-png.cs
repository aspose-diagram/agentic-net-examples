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
            // Ensure the diagram has exactly 10 pages (the default diagram starts with one page)
            while (diagram.Pages.Count < 10)
            {
                diagram.Pages.Add(new Page());
            }

            // Loop through each page, draw a diamond, and save the page as a PNG file
            for (int i = 0; i < 10; i++)
            {
                Page page = diagram.Pages[i];

                // Define the diamond shape using a flat double array (x, y pairs)
                // The shape is closed by returning to the starting point
                double[] diamondPoints = new double[]
                {
                    0, 0,    // start point (center)
                    2, 1,    // right middle
                    0, 2,    // top
                    -2, 1,   // left middle
                    0, 0     // back to start to close the diamond
                };

                // Draw the diamond polyline on the current page
                // The first two coordinates are the start point; the next two are the end point (same as start for a closed shape)
                page.DrawPolyline(0, 0, 0, 0, diamondPoints);

                // Configure PNG export options for the current page
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                pngOptions.PageIndex = i; // export only this page

                // Save the page as a PNG file
                string fileName = $"Diamond_Page_{i + 1}.png";
                diagram.Save(fileName, pngOptions);
            }
        }
    }
}
