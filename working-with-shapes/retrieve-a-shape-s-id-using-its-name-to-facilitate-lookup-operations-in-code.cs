using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string filePath = "sample.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // The shape name to search for (NameU is the universal name)
            string targetShapeName = "MyShape";

            // Variable to hold the found shape ID
            long foundShapeId = -1;

            // Iterate through all pages and shapes to locate the shape by name
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == targetShapeName)
                    {
                        foundShapeId = shape.ID;
                        break;
                    }
                }

                if (foundShapeId != -1)
                    break;
            }

            // Output the result
            if (foundShapeId != -1)
            {
                Console.WriteLine($"Shape '{targetShapeName}' found with ID: {foundShapeId}");
            }
            else
            {
                Console.WriteLine($"Shape '{targetShapeName}' not found in the diagram.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
