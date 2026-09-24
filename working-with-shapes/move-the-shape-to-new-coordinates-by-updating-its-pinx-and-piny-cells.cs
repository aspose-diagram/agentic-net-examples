using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Choose the page (first page in this example)
                Page page = diagram.Pages[0];

                // Identify the shape to move (shape with ID = 1 in this example)
                // Adjust the ID as needed for your specific shape
                long shapeId = 1;
                Shape shape = page.Shapes.GetShape(shapeId);

                // New coordinates (in inches)
                double newPinX = 5.0;
                double newPinY = 3.0;

                // Update the shape's position
                shape.XForm.PinX.Value = newPinX;
                shape.XForm.PinY.Value = newPinY;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }