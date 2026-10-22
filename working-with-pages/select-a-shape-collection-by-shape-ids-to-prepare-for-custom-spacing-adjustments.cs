using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define the input Visio file path.
        string inputPath = "Drawing1.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define the output Visio file path.
        string outputPath = "Drawing1_Updated.vsdx";

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Choose the page to work with (first page in this example).
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }
            Page page = diagram.Pages[0];

            // Define the shape IDs to select.
            long[] shapeIdsToSelect = new long[] { 15, 16, 20 };

            // Prepare a collection to hold the selected shapes.
            List<Shape> selectedShapes = new List<Shape>();

            // Iterate over each ID, retrieve the shape, and add to the collection if it exists and is not deleted.
            foreach (long shapeId in shapeIdsToSelect)
            {
                // Retrieve the shape by ID.
                Shape shape = page.Shapes.GetShape(shapeId);

                // Validate that the shape was found.
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on page \"{page.Name}\".");
                    continue;
                }

                // Skip logically deleted shapes.
                if (shape.Del == BOOL.True)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} is marked as deleted and will be ignored.");
                    continue;
                }

                // Add the valid shape to the selection list.
                selectedShapes.Add(shape);
                Console.WriteLine($"Selected shape ID {shapeId} (NameU: {shape.NameU}).");
            }

            // ------------------------------------------------------------
            // Custom spacing adjustments can be performed here.
            // For example, you might adjust the XForm.Width/Height or
            // reposition shapes based on your spacing algorithm.
            // ------------------------------------------------------------
            // Example placeholder (no actual spacing logic implemented):
            foreach (Shape shape in selectedShapes)
            {
                // Example: increase width by 0.5 inches.
                shape.XForm.Width.Value += 0.5;
                // Example: shift X position by 0.2 inches.
                shape.XForm.PinX.Value += 0.2;
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}