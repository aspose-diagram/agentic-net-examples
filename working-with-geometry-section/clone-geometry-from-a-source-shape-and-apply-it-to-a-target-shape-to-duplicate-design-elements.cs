using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram from a file
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Define the universal names of the source and target shapes
                string sourceShapeNameU = "SourceShape";
                string targetShapeNameU = "TargetShape";

                // Find the source shape on the first page
                Shape sourceShape = FindShapeByNameU(diagram.Pages[0], sourceShapeNameU);
                if (sourceShape == null)
                    throw new Exception($"Source shape with NameU '{sourceShapeNameU}' not found.");

                // Find the target shape on the first page
                Shape targetShape = FindShapeByNameU(diagram.Pages[0], targetShapeNameU);
                if (targetShape == null)
                    throw new Exception($"Target shape with NameU '{targetShapeNameU}' not found.");

                // Clone geometry (and other design elements) from source to target
                // The Copy method copies the source shape's data into the target shape
                targetShape.Copy(sourceShape);

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper method to locate a shape by its universal name (NameU) on a given page
        private static Shape FindShapeByNameU(Page page, string nameU)
        {
            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU != null && shape.NameU.Equals(nameU, StringComparison.OrdinalIgnoreCase))
                {
                    return shape;
                }
            }
            return null;
        }
    }