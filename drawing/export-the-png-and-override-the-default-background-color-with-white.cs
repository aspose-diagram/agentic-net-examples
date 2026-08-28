using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Get the first (foreground) page
                Page foregroundPage = diagram.Pages[0];

                // Retrieve page dimensions (in inches)
                double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

                // Create a new background page
                Page backgroundPage = new Page();
                backgroundPage.Background = BOOL.True;

                // Add a rectangle shape that spans the entire page
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;
                long rectShapeId = backgroundPage.AddShape(pinX, pinY, pageWidth, pageHeight, "Rectangle", false);
                Shape rectShape = backgroundPage.Shapes.GetShape(rectShapeId);

                // Set the rectangle fill to solid white
                rectShape.Fill.FillPattern.Value = 1;               // Solid fill
                rectShape.Fill.FillForegnd.Value = "#FFFFFF";       // White color
                rectShape.Line.LinePattern.Value = 0;               // No border

                // Add the background page to the diagram and link it
                diagram.Pages.Add(backgroundPage);
                foregroundPage.BackPage = backgroundPage;

                // Configure PNG export options
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                // Optional: exclude hidden pages if desired
                // pngOptions.ExportHiddenPage = false;

                // Export the diagram as PNG
                string outputPath = "output.png";
                diagram.Save(outputPath, pngOptions);

                Console.WriteLine($"Diagram exported to {outputPath} with a white background.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }