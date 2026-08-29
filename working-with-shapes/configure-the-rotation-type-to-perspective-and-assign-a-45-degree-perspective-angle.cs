using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Get the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page
                // Note: page.Shapes.GetShape expects an int ID; we use the first shape's ID
                if (page.Shapes.Count == 0)
                {
                    Console.WriteLine("No shapes found on the page.");
                    return;
                }

                // Get the first shape ID from the collection
                // Shapes are stored in a collection that can be iterated
                Aspose.Diagram.Shape firstShape = null;
                foreach (Aspose.Diagram.Shape shp in page.Shapes)
                {
                    firstShape = shp;
                    break;
                }

                if (firstShape == null)
                {
                    Console.WriteLine("Failed to retrieve a shape.");
                    return;
                }

                // Configure 3D rotation: set rotation type to Perspective
                firstShape.ThreeDFormat.RotationType.Value = RotationTypeValue.Perspective;

                // Assign a 45-degree perspective angle
                // The Perspective property expects a double value (in degrees)
                firstShape.ThreeDFormat.Perspective.Value = 45;

                // Save the modified diagram to a new file
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved with perspective rotation to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }