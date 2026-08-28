using System;
using System.IO;
using Aspose.Diagram;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Process each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // First, collect all shapes that are groups.
                // We store them in a separate list to avoid modifying the collection while iterating.
                List<Shape> groupShapes = new List<Shape>();
                foreach (Shape shape in page.Shapes)
                {
                    // A shape is a group if its Group property is not null.
                    if (shape.Group != null)
                    {
                        groupShapes.Add(shape);
                    }
                }

                // Ungroup each collected group shape.
                // After UnGroup, the members become individual shapes in the same collection.
                foreach (Shape groupShape in groupShapes)
                {
                    page.Shapes.UnGroup(groupShape);
                }
            }

            // Save the diagram with all groups flattened.
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
