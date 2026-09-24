using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // ID of the shape to extract (replace with the actual shape ID)
                long shapeId = 5;

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Assume the shape is on the first page; adjust if necessary
                Page page = diagram.Pages[0];

                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    throw new Exception($"Shape with ID {shapeId} not found.");
                }

                // Ensure the shape is not marked as deleted
                if (shape.Del == BOOL.True)
                {
                    throw new Exception($"Shape with ID {shapeId} is marked as deleted.");
                }

                // Configure PNG export options with 300 DPI resolution
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                saveOptions.Resolution = 300f; // DPI

                // Export the shape to a PNG file
                string outputPath = "shape_output.png";
                shape.ToImage(outputPath, saveOptions);

                Console.WriteLine($"Shape {shapeId} exported successfully to {outputPath} at 300 DPI.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }