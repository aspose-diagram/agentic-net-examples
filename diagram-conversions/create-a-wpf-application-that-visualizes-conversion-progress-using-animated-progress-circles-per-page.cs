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
                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    diagram.Pages.Add(new Page());
                }

                // Iterate through each page and add a progress circle (ellipse)
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Define circle geometry (center at (2,2), radius 1 inch)
                    double pinX = 2.0;   // Center X
                    double pinY = 2.0;   // Center Y
                    double width = 2.0;  // Diameter X
                    double height = 2.0; // Diameter Y

                    // Draw the ellipse; returns the shape ID (long)
                    long circleShapeId = page.DrawEllipse(pinX, pinY, width, height);

                    // Retrieve the shape object for further modifications
                    Shape circleShape = page.Shapes.GetShape(circleShapeId);

                    // Initial fill color (red) representing 0% progress
                    circleShape.Fill.FillForegnd.Value = "#FF0000";

                    // Simulate progress from 0% to 100% in steps of 20%
                    for (int progress = 0; progress <= 100; progress += 20)
                    {
                        // Update fill color based on progress (green at 100%)
                        // Simple linear interpolation between red (#FF0000) and green (#00FF00)
                        int red = (int)(255 * (100 - progress) / 100.0);
                        int green = (int)(255 * progress / 100.0);
                        string hexColor = $"#{red:X2}{green:X2}00";
                        circleShape.Fill.FillForegnd.Value = hexColor;

                        // Prepare image save options for PNG export of the current page
                        ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                        saveOptions.PageIndex = pageIndex; // Export only the current page

                        // Build output file name
                        string outputPath = $"Page{pageIndex + 1}_Progress{progress}.png";

                        // Save the diagram (only the specified page) as PNG
                        diagram.Save(outputPath, saveOptions);

                        Console.WriteLine($"Saved {outputPath} with progress {progress}%.");
                    }
                }

                // Optionally, save the final diagram with all circles at 100% progress
                diagram.Save("FinalDiagram.vsdx", SaveFileFormat.Vsdx);
                Console.WriteLine("Final diagram saved as FinalDiagram.vsdx.");
            }
        }
    }