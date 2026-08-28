using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Create a new empty diagram
            using (Diagram diagram = new Diagram())
            {
                // Define how many pages (e.g., documents) we want to visualize
                int pageCount = 3;

                // Add the required pages to the diagram
                for (int i = 0; i < pageCount; i++)
                {
                    Page page = new Page();
                    page.Name = $"Page{i + 1}";
                    diagram.Pages.Add(page);
                }

                // Number of animation steps for each progress circle
                int totalSteps = 10;

                // Iterate over each page and create an animated progress circle
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Define circle geometry (center at (5,5), radius 2 inches)
                    double centerX = 5.0;
                    double centerY = 5.0;
                    double radius = 2.0;
                    double left = centerX - radius;
                    double top = centerY - radius;
                    double diameter = radius * 2.0;

                    // Draw the circle once; keep the shape ID for later updates
                    long circleShapeId = page.DrawEllipse(left, top, diameter, diameter);
                    Shape circleShape = page.Shapes.GetShape(circleShapeId);

                    // Animate the circle by changing its fill color from red to green
                    for (int step = 1; step <= totalSteps; step++)
                    {
                        // Compute a simple red‑to‑green gradient based on progress
                        double ratio = (double)step / totalSteps; // 0.0 .. 1.0
                        int red = (int)((1.0 - ratio) * 255);
                        int green = (int)(ratio * 255);
                        string hexColor = $"#{red:X2}{green:X2}00"; // e.g., #FF0000 -> #00FF00

                        // Apply the computed color to the circle's fill
                        circleShape.Fill.FillForegnd.Value = hexColor;

                        // Prepare image export options for the current page
                        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png)
                        {
                            PageIndex = pageIndex,   // export only this page
                            PageCount = 1,
                            ExportHiddenPage = false
                        };

                        // Save the current animation frame as a PNG file
                        string outputFile = $"Page_{pageIndex + 1}_Step_{step}.png";
                        diagram.Save(outputFile, imgOptions);
                    }
                }
            }

            Console.WriteLine("Progress circle animation frames have been generated.");
        }
    }