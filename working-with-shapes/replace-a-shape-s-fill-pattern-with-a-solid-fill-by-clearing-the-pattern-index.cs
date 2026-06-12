using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram.
                // Replace "input.vsdx" with the actual file path.
                Diagram diagram = new Diagram("input.vsdx");

                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                    throw new Exception("The diagram contains no pages.");

                // Work with the first page.
                Page page = diagram.Pages[0];

                // Ensure the page has at least one shape.
                if (page.Shapes.Count == 0)
                    throw new Exception("The page contains no shapes.");

                // Retrieve the first shape on the page.
                // The Shapes collection indexer uses an int, while GetShape expects the shape's ID (long).
                Shape firstShape = page.Shapes[0];
                long shapeId = firstShape.ID;
                Shape shape = page.Shapes.GetShape(shapeId);

                // Set the fill pattern to solid.
                // In Visio, a FillPattern value of 1 represents a solid fill.
                shape.Fill.FillPattern.Value = 1;

                // Save the modified diagram.
                // Replace "output.vsdx" with the desired output path.
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }