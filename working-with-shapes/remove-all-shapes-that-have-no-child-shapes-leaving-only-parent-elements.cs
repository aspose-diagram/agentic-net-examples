using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Collect IDs of shapes that have no child shapes
                List<long> shapesToDelete = new List<long>();

                foreach (Shape shape in page.Shapes)
                {
                    // A shape is considered a parent if it contains child shapes (group shape)
                    // Non-group shapes or empty groups have a null or empty Shapes collection
                    if (shape.Shapes == null || shape.Shapes.Count == 0)
                    {
                        shapesToDelete.Add(shape.ID);
                    }
                }

                // Mark the identified shapes as deleted
                foreach (long shapeId in shapesToDelete)
                {
                    Shape s = page.Shapes.GetShape(shapeId);
                    s.Del = BOOL.True;
                }
            }

            // Save the modified diagram, preserving only parent (group) shapes
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
