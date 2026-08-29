using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ShapeBatchConverter
{
    static void Main(string[] args)
    {
        try
        {

            // Input folder containing Visio files (e.g., .vsdx, .vsd)
            string inputFolder = @"C:\VisioFiles";
            // Output folder where PNG images will be saved
            string outputFolder = @"C:\ShapeImages";

            // Ensure output directory exists
            Directory.CreateDirectory(outputFolder);

            // Get all Visio files in the input folder
            string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string visioPath in visioFiles)
            {
                // Load the diagram using the Diagram(string) constructor (load rule)
                using (Diagram diagram = new Diagram(visioPath))
                {
                    // Prepare high‑resolution image save options
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    // Set a high DPI (e.g., 300) for better quality
                    imgOptions.Resolution = 300;

                    // Iterate through all pages
                    for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                    {
                        var page = diagram.Pages[pageIndex];

                        // Iterate through all shapes on the page
                        for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                        {
                            Shape shape = page.Shapes[shapeIndex];

                            // Build a unique file name: DiagramName_PageIndex_ShapeID.png
                            string diagramName = Path.GetFileNameWithoutExtension(visioPath);
                            string fileName = $"{diagramName}_Page{pageIndex + 1}_Shape{shape.ID}.png";
                            string outputPath = Path.Combine(outputFolder, fileName);

                            // Export the shape to PNG using the ToImage method (export rule)
                            shape.ToImage(outputPath, imgOptions);
                        }
                    }
                }
            }

            Console.WriteLine("All shapes have been exported as high‑resolution PNG files.");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
