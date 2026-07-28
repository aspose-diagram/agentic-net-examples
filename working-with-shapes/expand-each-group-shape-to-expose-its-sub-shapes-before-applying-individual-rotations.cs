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

                // Load the Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Process each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // First, collect IDs of all group shapes on the page
                    List<long> groupShapeIds = new List<long>();
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Type == TypeValue.Group)
                        {
                            groupShapeIds.Add(shape.ID);
                        }
                    }

                    // Expand (ungroup) each group shape to expose its sub‑shapes
                    foreach (long groupId in groupShapeIds)
                    {
                        Shape groupShape = page.Shapes.GetShape(groupId);
                        if (groupShape != null)
                        {
                            groupShape.Ungroup();
                        }
                    }

                    // After ungrouping, rotate each non‑connector shape individually
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip connectors (1‑D shapes)
                        if (shape.OneD)
                            continue;

                        // Apply a rotation of 30 degrees (Angle cell expects degrees)
                        shape.XForm.Angle.Value = 30;
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }