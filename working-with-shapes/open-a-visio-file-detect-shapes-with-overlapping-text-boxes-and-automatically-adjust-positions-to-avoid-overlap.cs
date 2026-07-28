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

                // Process each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Collect non-deleted shapes on the page
                    List<Shape> shapes = new List<Shape>();
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Del == BOOL.False)
                        {
                            shapes.Add(shape);
                        }
                    }

                    // Simple pairwise overlap detection and resolution
                    for (int i = 0; i < shapes.Count; i++)
                    {
                        Shape shapeA = shapes[i];
                        double aLeft = shapeA.XForm.PinX.Value - shapeA.XForm.Width.Value / 2;
                        double aRight = shapeA.XForm.PinX.Value + shapeA.XForm.Width.Value / 2;
                        double aTop = shapeA.XForm.PinY.Value + shapeA.XForm.Height.Value / 2;
                        double aBottom = shapeA.XForm.PinY.Value - shapeA.XForm.Height.Value / 2;

                        for (int j = i + 1; j < shapes.Count; j++)
                        {
                            Shape shapeB = shapes[j];
                            double bLeft = shapeB.XForm.PinX.Value - shapeB.XForm.Width.Value / 2;
                            double bRight = shapeB.XForm.PinX.Value + shapeB.XForm.Width.Value / 2;
                            double bTop = shapeB.XForm.PinY.Value + shapeB.XForm.Height.Value / 2;
                            double bBottom = shapeB.XForm.PinY.Value - shapeB.XForm.Height.Value / 2;

                            // Check for overlap
                            bool overlap = !(aRight <= bLeft || aLeft >= bRight || aTop <= bBottom || aBottom >= bTop);
                            if (overlap)
                            {
                                // Move shapeB to the right by the width of shapeA plus a small margin
                                double offset = shapeA.XForm.Width.Value + 0.5;
                                shapeB.XForm.PinX.Value += offset;

                                // Update bounding values for subsequent checks
                                bLeft += offset;
                                bRight += offset;

                                Console.WriteLine($"Adjusted shape ID {shapeB.ID} to avoid overlap with shape ID {shapeA.ID}.");
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }