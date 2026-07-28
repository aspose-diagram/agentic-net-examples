using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: first diagram path, second diagram path to merge, output path
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: DiagramProcessing <baseDiagram.vsdx> <mergeDiagram.vsdx> <outputDiagram.vsdx>");
                return;
            }

            string basePath = args[0];
            string mergePath = args[1];
            string outputPath = args[2];

            // Load the base diagram
            Diagram baseDiagram = new Diagram(basePath);

            // Load the diagram to be merged
            Diagram mergeDiagram = new Diagram(mergePath);

            // Combine the second diagram into the first one
            baseDiagram.Combine(mergeDiagram);

            // Flatten all group shapes into individual shapes
            foreach (Page page in baseDiagram.Pages)
            {
                // Collect IDs of group shapes first to avoid modifying the collection during iteration
                List<long> groupShapeIds = new List<long>();
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Type == TypeValue.Group)
                    {
                        groupShapeIds.Add(shape.ID);
                    }
                }

                // Ungroup each collected group shape
                foreach (long groupId in groupShapeIds)
                {
                    Shape groupShape = page.Shapes.GetShape(groupId);
                    if (groupShape != null)
                    {
                        groupShape.Ungroup();
                    }
                }
            }

            // Save the resulting diagram
            baseDiagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
    }