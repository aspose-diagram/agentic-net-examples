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

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Collect shapes that have no child shapes
                List<Shape> shapesToRemove = new List<Shape>();
                foreach (Shape shape in page.Shapes)
                {
                    // shape.Shapes holds child shapes; if count is zero, it's a leaf shape
                    if (shape.Shapes == null || shape.Shapes.Count == 0)
                    {
                        shapesToRemove.Add(shape);
                    }
                }

                // Remove the collected leaf shapes from the page
                foreach (Shape shape in shapesToRemove)
                {
                    page.Shapes.Remove(shape);
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
