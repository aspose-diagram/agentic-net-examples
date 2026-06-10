using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Path for the protected output file
                string outputPath = "output_protected.vsdx";

                // Area threshold (in square inches) – shapes larger than this will be protected
                double areaThreshold = 4.0; // example: 2" x 2" = 4 sq.in.

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve width and height (in inches) from the shape's XForm
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Calculate area
                        double area = width * height;

                        // Apply protection if area exceeds the threshold
                        if (area > areaThreshold)
                        {
                            // Lock movement
                            shape.Protection.LockMoveX.Value = BOOL.True;
                            shape.Protection.LockMoveY.Value = BOOL.True;

                            // Lock resizing
                            shape.Protection.LockWidth.Value = BOOL.True;
                            shape.Protection.LockHeight.Value = BOOL.True;

                            // Lock rotation
                            shape.Protection.LockRotate.Value = BOOL.True;

                            // Lock vertex editing
                            shape.Protection.LockVtxEdit.Value = BOOL.True;
                        }
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