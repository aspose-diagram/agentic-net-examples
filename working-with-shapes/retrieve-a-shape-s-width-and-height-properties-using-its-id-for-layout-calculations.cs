using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file
                string diagramPath = "example.vsdx";

                // Shape ID to retrieve (replace with actual ID)
                long shapeId = 5;

                // Load the diagram from a file stream
                using (FileStream stream = new FileStream(diagramPath, FileMode.Open, FileAccess.Read))
                {
                    Diagram diagram = new Diagram(stream);

                    // Access the first page (index 0)
                    Page page = diagram.Pages[0];

                    // Retrieve the shape by its ID
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Get width and height (values are in inches)
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;

                    // Output the dimensions
                    Console.WriteLine($"Shape ID: {shapeId}");
                    Console.WriteLine($"Width: {width} inches");
                    Console.WriteLine($"Height: {height} inches");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }