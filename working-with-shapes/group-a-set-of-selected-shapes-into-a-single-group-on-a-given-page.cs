using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Choose the page where the shapes reside (e.g., first page)
                Page page = diagram.Pages[0];

                // IDs of the shapes you want to group.
                // These IDs should correspond to existing shapes on the page.
                long shapeId1 = 1; // replace with actual shape ID
                long shapeId2 = 2; // replace with actual shape ID
                long shapeId3 = 3; // replace with actual shape ID

                // Retrieve the Shape objects by their IDs
                Shape shape1 = page.Shapes.GetShape(shapeId1);
                Shape shape2 = page.Shapes.GetShape(shapeId2);
                Shape shape3 = page.Shapes.GetShape(shapeId3);

                // Group the selected shapes into a single group shape
                Shape[] shapesToGroup = new Shape[] { shape1, shape2, shape3 };
                Shape groupShape = page.Shapes.Group(shapesToGroup);

                // Optional: set a name for the new group for easier identification
                groupShape.Name = "MyGroup";
                groupShape.NameU = "MyGroup";

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up resources
                diagram.Dispose();

                Console.WriteLine("Shapes have been grouped and diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }