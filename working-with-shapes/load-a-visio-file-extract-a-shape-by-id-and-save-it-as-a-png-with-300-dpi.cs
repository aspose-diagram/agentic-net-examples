using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Identifier of the shape to extract (replace with actual ID)
                long shapeId = 123;

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Attempt to locate the shape on the first page
                // Adjust page index if the shape resides on a different page
                Page page = diagram.Pages[0];
                Shape shape = page.Shapes.GetShape(shapeId);

                if (shape == null)
                {
                    throw new Exception($"Shape with ID {shapeId} was not found on page {page.Name}.");
                }

                // Configure PNG export options with 300 DPI resolution
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                pngOptions.Resolution = 300f; // DPI

                // Output file path for the extracted shape image
                string outputPath = "shape.png";

                // Export the shape to PNG using the specified options
                shape.ToImage(outputPath, pngOptions);

                Console.WriteLine($"Shape {shapeId} has been saved as PNG to '{outputPath}' with 300 DPI.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }