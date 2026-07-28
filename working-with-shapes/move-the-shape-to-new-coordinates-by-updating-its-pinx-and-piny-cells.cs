using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Choose the page (first page in this example)
                Page page = diagram.Pages[0];

                // ID of the shape to move (replace with the actual shape ID)
                long shapeId = 5;

                // Retrieve the shape; GetShape expects an int, so cast the long ID
                Shape shape = page.Shapes.GetShape((int)shapeId);
                if (shape == null)
                {
                    throw new Exception($"Shape with ID {shapeId} not found on page '{page.Name}'.");
                }

                // New coordinates (in inches)
                double newPinX = 5.0;
                double newPinY = 7.0;

                // Update the shape's position
                shape.XForm.PinX.Value = newPinX;
                shape.XForm.PinY.Value = newPinY;

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Shape {shapeId} moved to ({newPinX}, {newPinY}) and saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }