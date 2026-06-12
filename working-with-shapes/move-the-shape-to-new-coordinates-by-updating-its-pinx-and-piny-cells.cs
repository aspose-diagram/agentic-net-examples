using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // New coordinates for the shape (in inches)
                double newPinX = 5.0;
                double newPinY = 3.0;

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Locate the shape to move (example: by its universal name)
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == "MyShape")
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.WriteLine("Shape 'MyShape' not found.");
                    return;
                }

                // Update the shape's position by setting PinX and PinY cells
                targetShape.XForm.PinX.Value = newPinX;
                targetShape.XForm.PinY.Value = newPinY;

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Shape moved to ({newPinX}, {newPinY}) and saved to {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }