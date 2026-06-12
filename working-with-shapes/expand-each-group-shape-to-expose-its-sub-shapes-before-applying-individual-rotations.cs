using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // First, collect IDs of all group shapes on the current page
                    List<long> groupShapeIds = new List<long>();
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Type == TypeValue.Group)
                        {
                            groupShapeIds.Add(shape.ID);
                        }
                    }

                    // Ungroup each group shape to expose its sub‑shapes
                    foreach (long groupId in groupShapeIds)
                    {
                        Shape groupShape = page.Shapes.GetShape(groupId);
                        if (groupShape != null)
                        {
                            // Ungroup expands the group; the original group shape is removed
                            groupShape.Ungroup();
                        }
                    }

                    // After ungrouping, apply an individual rotation to each non‑group shape
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip any remaining group shapes (if any)
                        if (shape.Type == TypeValue.Group)
                            continue;

                        // Set rotation angle (degrees). Example: rotate 45 degrees.
                        shape.XForm.Angle.Value = 45;
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