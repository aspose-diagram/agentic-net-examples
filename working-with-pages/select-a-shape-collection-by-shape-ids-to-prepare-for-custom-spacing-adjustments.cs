using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Define the shape IDs you want to work with
            long[] shapeIds = new long[] { 1, 2, 3 }; // replace with actual IDs

            // Collect the shapes corresponding to the specified IDs
            List<Shape> selectedShapes = new List<Shape>();
            foreach (long id in shapeIds)
            {
                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(id);
                if (shape != null)
                {
                    selectedShapes.Add(shape);
                    Console.WriteLine($"Selected shape ID {id}, NameU: {shape.NameU}");
                }
                else
                {
                    Console.WriteLine($"Shape with ID {id} not found on the page.");
                }
            }

            // At this point you have a collection (selectedShapes) ready for any custom spacing adjustments.
            // Example placeholder: adjust positions, spacing, etc.

            // Save the modified diagram to a new file
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
