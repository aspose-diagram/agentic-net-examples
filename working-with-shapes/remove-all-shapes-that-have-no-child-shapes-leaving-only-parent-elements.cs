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

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Collect IDs of shapes that are NOT group shapes (i.e., have no child shapes)
                List<long> idsToDelete = new List<long>();
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Type != TypeValue.Group)
                    {
                        idsToDelete.Add(shape.ID);
                    }
                }

                // Mark the collected shapes for deletion
                foreach (long shapeId in idsToDelete)
                {
                    Shape shape = page.Shapes.GetShape(shapeId);
                    shape.Del = BOOL.True;
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
