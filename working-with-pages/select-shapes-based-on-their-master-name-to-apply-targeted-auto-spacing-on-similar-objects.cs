using System;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the master name to target for auto‑spacing
                string targetMasterName = "Rectangle";

                // Define the desired horizontal spacing (in inches)
                double horizontalSpacing = 0.5;

                // Process each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Collect shape IDs that use the specified master
                    var shapeIds = new System.Collections.Generic.List<long>();
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Master != null && shape.Master.Name == targetMasterName)
                        {
                            shapeIds.Add(shape.ID);
                        }
                    }

                    // If fewer than two shapes match, no spacing is needed
                    if (shapeIds.Count < 2)
                        continue;

                    // Sort the collected shapes by their current PinX (horizontal position)
                    shapeIds.Sort((id1, id2) =>
                    {
                        Shape s1 = page.Shapes.GetShape(id1);
                        Shape s2 = page.Shapes.GetShape(id2);
                        return s1.XForm.PinX.Value.CompareTo(s2.XForm.PinX.Value);
                    });

                    // Apply spacing sequentially
                    for (int i = 1; i < shapeIds.Count; i++)
                    {
                        Shape previous = page.Shapes.GetShape(shapeIds[i - 1]);
                        Shape current = page.Shapes.GetShape(shapeIds[i]);

                        // Calculate the rightmost edge of the previous shape
                        double previousRightEdge = previous.XForm.PinX.Value + (previous.XForm.Width.Value / 2.0);

                        // Desired left edge for the current shape
                        double desiredLeftEdge = previousRightEdge + horizontalSpacing;

                        // Set the new PinX for the current shape so that its left edge aligns with desiredLeftEdge
                        current.XForm.PinX.Value = desiredLeftEdge + (current.XForm.Width.Value / 2.0);
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }