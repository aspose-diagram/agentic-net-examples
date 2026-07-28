using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Choose the page (first page in this example)
                Page page = diagram.Pages[0];

                // Identify the shape to modify (shape with ID = 1 in this example)
                // Adjust the ID as needed for your specific diagram.
                long shapeId = 1;
                Shape shape = page.Shapes.GetShape(shapeId);

                if (shape == null)
                {
                    Console.WriteLine($"Shape with ID {shapeId} not found.");
                    return;
                }

                // Preserve the current Y coordinate (PinY)
                double currentPinY = shape.XForm.PinY.Value;

                // Set the new X coordinate (PinX) while keeping Y unchanged
                double newPinX = 5.0; // Desired X coordinate in inches
                shape.XForm.PinX.Value = newPinX;
                shape.XForm.PinY.Value = currentPinY; // Explicitly keep Y the same (optional)

                Console.WriteLine($"Shape ID {shapeId} X coordinate set to {newPinX} inches (Y remains {currentPinY} inches).");

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }