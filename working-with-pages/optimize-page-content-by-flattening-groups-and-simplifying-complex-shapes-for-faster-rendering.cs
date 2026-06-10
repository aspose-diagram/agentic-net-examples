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
                using (Diagram diagram = new Diagram("input.vsdx"))
                {
                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // ---------- Flatten group shapes ----------
                        // Collect group shapes first to avoid modifying the collection during iteration
                        List<Shape> groups = new List<Shape>();
                        foreach (Shape shape in page.Shapes)
                        {
                            if (shape.Type == TypeValue.Group)
                            {
                                groups.Add(shape);
                            }
                        }

                        // Ungroup each collected group shape
                        foreach (Shape groupShape in groups)
                        {
                            // Ungroup expands the group into its constituent shapes
                            groupShape.Ungroup();
                        }

                        // ---------- Simplify overly complex shapes ----------
                        // Define a threshold for the number of geometry elements that makes a shape "complex"
                        const int geometryThreshold = 20;
                        List<Shape> complexShapes = new List<Shape>();

                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip group shapes (already handled) and deleted shapes
                            if (shape.Type == TypeValue.Group || shape.Del == BOOL.True)
                                continue;

                            // Identify complex shapes by the count of geometry elements
                            if (shape.Geoms != null && shape.Geoms.Count > geometryThreshold)
                            {
                                complexShapes.Add(shape);
                            }
                        }

                        // Replace each complex shape with a simple rectangle that matches its bounding box
                        foreach (Shape complexShape in complexShapes)
                        {
                            // Retrieve the shape's position and size
                            double pinX = complexShape.XForm.PinX.Value;
                            double pinY = complexShape.XForm.PinY.Value;
                            double width = complexShape.XForm.Width.Value;
                            double height = complexShape.XForm.Height.Value;

                            // Draw a simple rectangle at the same location
                            long rectId = page.DrawRectangle(pinX, pinY, width, height);
                            // Optionally, copy basic formatting (fill, line) from the original shape
                            Shape rectShape = page.Shapes.GetShape(rectId);
                            rectShape.Fill.FillForegnd.Value = complexShape.Fill.FillForegnd.Value;
                            rectShape.Line.LineColor.Value = complexShape.Line.LineColor.Value;
                            rectShape.Line.LineWeight.Value = complexShape.Line.LineWeight.Value;

                            // Remove the original complex shape from the page
                            page.Shapes.Remove(complexShape);
                        }
                    }

                    // Save the optimized diagram
                    diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }