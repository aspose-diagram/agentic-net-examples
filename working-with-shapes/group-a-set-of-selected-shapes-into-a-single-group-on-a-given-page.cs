using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Choose the page where grouping will be performed (first page in this example)
                    Page page = diagram.Pages[0];

                    // IDs of the shapes to be grouped (replace with actual IDs as needed)
                    long[] shapeIdsToGroup = new long[] { 1, 2, 3 };

                    // Collect the Shape objects corresponding to the IDs
                    List<Shape> shapesToGroup = new List<Shape>();
                    foreach (long shapeId in shapeIdsToGroup)
                    {
                        // Retrieve the shape by its ID
                        Shape shape = page.Shapes.GetShape(shapeId);
                        // Ensure the shape exists and is not marked as deleted
                        if (shape != null && shape.Del == BOOL.False)
                        {
                            shapesToGroup.Add(shape);
                        }
                    }

                    // Verify that we have at least two shapes to form a group
                    if (shapesToGroup.Count < 2)
                    {
                        Console.WriteLine("Not enough shapes to create a group.");
                        return;
                    }

                    // Create the group from the selected shapes
                    Shape groupShape = page.Shapes.Group(shapesToGroup.ToArray());

                    // Optionally set a name for the new group
                    groupShape.Name = "MyGroupedShape";

                    // Save the modified diagram
                    string outputPath = "output.vsdx";
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);

                    Console.WriteLine($"Shapes grouped successfully and saved to '{outputPath}'.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }