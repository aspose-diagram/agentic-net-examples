using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Remove the default page if it exists to start with a clean collection
            if (diagram.Pages.Count > 0)
            {
                Page defaultPage = diagram.Pages[0];
                diagram.Pages.Remove(defaultPage);
            }

            // Add ten pages and draw a diamond on each
            for (int i = 0; i < 10; i++)
            {
                // Determine the next page ID (max existing ID + 1)
                int maxId = 0;
                foreach (Page existingPage in diagram.Pages)
                {
                    if (existingPage.ID > maxId)
                        maxId = existingPage.ID;
                }

                // Create and configure the new page
                Page newPage = new Page(maxId + 1);
                newPage.Name = $"Page{i + 1}";
                diagram.Pages.Add(newPage);

                // Define diamond vertices (top, right, bottom, left, back to top)
                double[] diamondPoints = new double[]
                {
                    5.0, 6.0,   // Top
                    6.0, 5.0,   // Right
                    5.0, 4.0,   // Bottom
                    4.0, 5.0,   // Left
                    5.0, 6.0    // Close polygon
                };

                // Draw the diamond shape on the current page
                long shapeId = newPage.DrawPolyline(diamondPoints);
                // Shape retrieval is optional; the shape is already part of the page
                // Shape diamondShape = newPage.Shapes.GetShape(shapeId);
            }

            // Export each page as an individual PNG file
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                pngOptions.PageIndex = i;   // Export only the current page
                pngOptions.PageCount = 1;   // Single page per file

                string outputPath = $"DiamondPage{i + 1}.png";
                diagram.Save(outputPath, pngOptions);
                Console.WriteLine($"Saved {outputPath}");
            }

            // Dispose the diagram to release resources
            diagram.Dispose();
        }
    }