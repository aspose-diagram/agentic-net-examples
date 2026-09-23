using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file (must exist)
                string inputPath = "input.vsdx";

                // Path for the resulting Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Get the first page (index 0)
                Page page = diagram.Pages[0];

                // Add a few sample shapes using the built‑in "Rectangle" master
                // The fourth parameter (isCalculate) must be a boolean
                long shapeId1 = page.AddShape(2.0, 2.0, "Rectangle", false);
                long shapeId2 = page.AddShape(4.0, 2.0, "Rectangle", false);
                long shapeId3 = page.AddShape(6.0, 2.0, "Rectangle", false);

                // Retrieve the shapes (optional – shown for completeness)
                Shape shape1 = page.Shapes.GetShape(shapeId1);
                Shape shape2 = page.Shapes.GetShape(shapeId2);
                Shape shape3 = page.Shapes.GetShape(shapeId3);

                // Set some simple text on each shape
                shape1.Text.Value.Clear();
                shape1.Text.Value.Add(new Txt("Shape 1"));
                shape2.Text.Value.Clear();
                shape2.Text.Value.Add(new Txt("Shape 2"));
                shape3.Text.Value.Clear();
                shape3.Text.Value.Add(new Txt("Shape 3"));

                // Configure AutoSpaceOptions with negative distances to force overlap
                AutoSpaceOptions autoSpace = new AutoSpaceOptions
                {
                    // Negative values cause shapes to move closer together (overlap)
                    DistanceInHorizontal = -0.5, // inches
                    DistanceInVertical = -0.5    // inches
                };

                // Apply the auto‑spacing to all shapes on the page
                page.AutoSpaceShapes(page.Shapes, autoSpace);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }