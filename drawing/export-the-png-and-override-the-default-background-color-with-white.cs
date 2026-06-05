using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Get the first (foreground) page
                Page foregroundPage = diagram.Pages[0];

                // Retrieve page dimensions (in inches)
                double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

                // Create a new background page
                Page backgroundPage = new Page();
                backgroundPage.Background = BOOL.True;
                backgroundPage.PageSheet.PageProps.PageWidth.Value = pageWidth;
                backgroundPage.PageSheet.PageProps.PageHeight.Value = pageHeight;

                // Calculate center position for the rectangle that will cover the whole page
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Add a rectangle shape that spans the entire page
                // isCalculate = false (no automatic size calculation)
                long rectShapeId = backgroundPage.AddShape(pinX, pinY, pageWidth, pageHeight, "Rectangle", false);
                Shape rectShape = backgroundPage.Shapes.GetShape(rectShapeId);

                // Set solid fill pattern and white color
                rectShape.Fill.FillPattern.Value = 1;               // Solid fill
                rectShape.Fill.FillForegnd.Value = "#FFFFFF";       // White color

                // Remove outline
                rectShape.Line.LinePattern.Value = 0;               // No line

                // Send the rectangle to the back so other content appears above it
                rectShape.SendToBack();

                // Make the background shape non‑selectable
                rectShape.Protection.LockSelect.Value = BOOL.True;

                // Attach the background page to the foreground page
                foregroundPage.BackPage = backgroundPage;

                // Add the background page to the diagram's page collection
                diagram.Pages.Add(backgroundPage);

                // Configure PNG export options
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);

                // Export the diagram as PNG with the overridden white background
                string outputPath = "output.png";
                diagram.Save(outputPath, pngOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }