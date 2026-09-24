using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Select the page (first page in this example)
                Page page = diagram.Pages[0];

                // Specify the shape ID you want to inspect.
                // Replace this with the actual ID of the shape you are interested in.
                long shapeId = 1;

                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);

                if (shape == null)
                {
                    Console.WriteLine($"Shape with ID {shapeId} not found.");
                    return;
                }

                // Read XForm properties
                double pinX = shape.XForm.PinX.Value;
                double pinY = shape.XForm.PinY.Value;
                double width = shape.XForm.Width.Value;
                double height = shape.XForm.Height.Value;

                // Output the values
                Console.WriteLine($"Shape ID: {shapeId}");
                Console.WriteLine($"PinX: {pinX}");
                Console.WriteLine($"PinY: {pinY}");
                Console.WriteLine($"Width: {width}");
                Console.WriteLine($"Height: {height}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }