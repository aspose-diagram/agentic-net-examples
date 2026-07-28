using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "optimized.vsdx";

                // Load the diagram using a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Collect IDs of group shapes to avoid modifying the collection while iterating
                        var groupShapeIds = new System.Collections.Generic.List<long>();

                        foreach (Shape shape in page.Shapes)
                        {
                            // Identify group shapes
                            if (shape.Type == TypeValue.Group)
                            {
                                groupShapeIds.Add(shape.ID);
                            }
                            else
                            {
                                // Simplify complex (non‑group) shapes:
                                // Set line pattern to solid
                                shape.Line.LinePattern.Value = LinePatternValue.Solid;
                                // Remove fill pattern (set to none)
                                shape.Fill.FillPattern.Value = 0;
                            }
                        }

                        // Ungroup each identified group shape
                        foreach (long groupId in groupShapeIds)
                        {
                            Shape groupShape = page.Shapes.GetShape(groupId);
                            if (groupShape != null)
                            {
                                // Ungroup expands the group into its constituent shapes
                                groupShape.Ungroup();
                            }
                        }
                    }

                    // Save the optimized diagram in VSDX format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Diagram optimization completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }