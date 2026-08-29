using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "sample.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Choose the page (first page in this example)
                Page page = diagram.Pages[0];

                // The shape ID we want to retrieve (example value)
                long shapeId = 12345;

                try
                {
                    // Attempt to get the shape by its ID
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // If GetShape returns null (unlikely), handle it explicitly
                    if (shape == null)
                    {
                        Console.WriteLine($"Shape with ID {shapeId} was not found (null returned).");
                    }
                    else
                    {
                        Console.WriteLine($"Shape found: ID={shape.ID}, Name={shape.Name}");
                        // Additional processing on the shape can be placed here
                    }
                }
                catch (Exception ex)
                {
                    // Catch any exception that occurs when the shape ID does not exist
                    Console.WriteLine($"Error retrieving shape with ID {shapeId}: {ex.Message}");
                }

                // Optional: save the diagram after any modifications
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }