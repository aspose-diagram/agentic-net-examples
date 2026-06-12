using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file (replace with actual file path)
                string diagramPath = "input.vsdx";

                // The ID of the shape whose dimensions are required (replace with actual ID)
                long shapeId = 5;

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Access the first page (adjust if needed)
                Page page = diagram.Pages[0];

                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Get width and height (values are in inches)
                double width = shape.XForm.Width.Value;
                double height = shape.XForm.Height.Value;

                // Output the dimensions
                Console.WriteLine($"Shape ID {shapeId}: Width = {width} inches, Height = {height} inches");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }