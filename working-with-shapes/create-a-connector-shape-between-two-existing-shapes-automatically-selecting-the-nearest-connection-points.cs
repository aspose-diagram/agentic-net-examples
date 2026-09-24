using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths (replace with actual paths)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the existing Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Find the IDs of the two shapes to connect (by their universal names)
                long shapeId1 = FindShapeIdByName(page, "Shape1");
                long shapeId2 = FindShapeIdByName(page, "Shape2");

                // Add a dynamic connector shape to the page
                // PinX and PinY are set to 0; the connector will be repositioned automatically when connected
                long connectorId = page.AddShape(0, 0, "Dynamic connector", false);

                // Connect the two shapes using the nearest connection points.
                // Here we use Bottom of the first shape and Top of the second shape as examples.
                page.ConnectShapesViaConnector(shapeId1, ConnectionPointPlace.Bottom, shapeId2, ConnectionPointPlace.Top, connectorId);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper method to locate a shape by its universal name and return its ID
        static long FindShapeIdByName(Page page, string nameU)
        {
            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU == nameU)
                {
                    return shape.ID;
                }
            }

            throw new Exception($"Shape with NameU '{nameU}' not found on the page.");
        }
    }