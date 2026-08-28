using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first (default) page
            Page foregroundPage = diagram.Pages[0];

            // Create a background page
            Page backgroundPage = new Page();
            backgroundPage.Background = BOOL.True; // Mark as background page
            diagram.Pages.Add(backgroundPage);

            // Link the foreground page to the background page
            foregroundPage.BackPage = backgroundPage;

            // Retrieve page dimensions (in inches)
            double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

            // Calculate center coordinates for the rectangle shape
            double centerX = pageWidth / 2.0;
            double centerY = pageHeight / 2.0;

            // Draw a rectangle that covers the entire page on the background page
            long rectShapeId = backgroundPage.DrawRectangle(centerX, centerY, pageWidth, pageHeight);
            Shape rectShape = backgroundPage.Shapes.GetShape(rectShapeId);

            // Set solid fill pattern and desired background color (hex string)
            rectShape.Fill.FillPattern.Value = 1; // Solid fill
            rectShape.Fill.FillForegnd.Value = "#ADD8E6"; // Light blue background

            // Remove outline by setting line pattern to none and line weight to zero
            rectShape.Line.LinePattern.Value = (LinePatternValue)0; // No line pattern
            rectShape.Line.LineWeight.Value = 0.0;

            // Send the rectangle to the back so other shapes appear above it
            rectShape.SendToBack();

            // Configure PNG export options
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            pngOptions.PageIndex = 0; // Export the first page (foreground page)
            pngOptions.ExportHiddenPage = false; // Do not export hidden pages

            // Save the diagram as a PNG file with the specified background color
            string outputPath = "ExportedDiagram.png";
            diagram.Save(outputPath, pngOptions);

            Console.WriteLine($"Diagram exported successfully to '{outputPath}'.");
        }
    }