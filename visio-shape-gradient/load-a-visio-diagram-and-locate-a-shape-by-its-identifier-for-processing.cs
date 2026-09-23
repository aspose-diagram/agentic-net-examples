using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file to be loaded
                string inputPath = "example.vsdx";

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Identifier of the shape to locate (example ID)
                long targetShapeId = 5; // replace with the actual shape ID

                // Retrieve the first page (adjust if the shape is on a different page)
                Page page = diagram.Pages[0];

                // Locate the shape by its ID
                Shape shape = page.Shapes.GetShape(targetShapeId);

                // Verify that the shape was found
                if (shape == null)
                {
                    throw new Exception($"Shape with ID {targetShapeId} was not found on page '{page.Name}'.");
                }

                // Example processing: output shape details
                Console.WriteLine($"Shape ID: {shape.ID}");
                Console.WriteLine($"Shape NameU: {shape.NameU}");
                Console.WriteLine($"Master Name: {shape.Master?.Name ?? "No master"}");
                Console.WriteLine($"Position (PinX, PinY): ({shape.XForm.PinX.Value}, {shape.XForm.PinY.Value})");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }