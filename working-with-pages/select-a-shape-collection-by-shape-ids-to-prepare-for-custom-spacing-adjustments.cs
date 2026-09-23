using System.IO;
using Aspose.Diagram;
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // IDs of the shapes you want to work with
            int[] shapeIds = new int[] { 1, 3, 5 };

            // Collection that will hold the selected shapes
            List<Shape> selectedShapes = new List<Shape>();

            // Iterate through all pages and shapes to find matches
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the current shape's ID is in the target list
                    if (Array.IndexOf(shapeIds, shape.ID) >= 0)
                    {
                        selectedShapes.Add(shape);
                    }
                }
            }

            // At this point 'selectedShapes' contains the shapes with the specified IDs.
            // You can now apply custom spacing adjustments to these shapes.

            // Example: adjust the X and Y positions (custom spacing)
            // foreach (Shape s in selectedShapes)
            // {
            //     s.XForm.PinX.Value += 0.5; // shift right
            //     s.XForm.PinY.Value += 0.5; // shift down
            // }

            // Save the diagram after any modifications
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
