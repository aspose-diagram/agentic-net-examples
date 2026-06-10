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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx"; // replace with your file path
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Define the shape IDs you want to work with
            long[] shapeIds = new long[] { 1, 2, 3 }; // replace with actual IDs

            // Collect the shapes into a list for further processing
            List<Shape> selectedShapes = new List<Shape>();
            foreach (long id in shapeIds)
            {
                // Retrieve shape by ID; GetShape returns null if not found
                Shape shape = page.Shapes.GetShape(id);
                if (shape != null && shape.Del == BOOL.False) // ensure the shape is not deleted
                {
                    selectedShapes.Add(shape);
                    Console.WriteLine($"Selected shape ID: {shape.ID}, NameU: {shape.NameU}");
                }
                else
                {
                    Console.WriteLine($"Shape with ID {id} not found or is marked as deleted.");
                }
            }

            // At this point 'selectedShapes' contains the shapes you can adjust spacing for.
            // Example placeholder for custom spacing logic:
            // foreach (Shape s in selectedShapes) { /* adjust s.XForm.PinX, s.XForm.PinY, etc. */ }

            // Save the diagram after any modifications (optional)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
