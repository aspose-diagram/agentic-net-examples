using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the source diagram and the output file
                string inputPath = "input.vsdx";
                string outputPath = "output_resized.vsdx";

                // Desired dimensions (in inches)
                double newWidth = 2.0;
                double newHeight = 1.0;

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Identify the shape to resize (example shape ID = 1)
                long shapeId = 1;
                Shape shape = page.Shapes.GetShape(shapeId);

                // Verify the shape exists and is not marked as deleted
                if (shape != null && shape.Del == BOOL.False)
                {
                    // Apply new width and height
                    shape.XForm.Width.Value = newWidth;
                    shape.XForm.Height.Value = newHeight;
                    Console.WriteLine($"Resized shape ID {shapeId} to {newWidth} x {newHeight} inches.");
                }
                else
                {
                    Console.WriteLine($"Shape with ID {shapeId} not found or is deleted.");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }