using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the input Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file
                string outputPath = "output_protected.vsdx";

                // Define the area threshold (in square inches)
                double areaThreshold = 5.0;

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve width and height (in inches)
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Calculate area
                        double area = width * height;

                        // Apply protection if area exceeds the threshold
                        if (area > areaThreshold)
                        {
                            shape.Protection.LockMoveX.Value = BOOL.True;
                            shape.Protection.LockMoveY.Value = BOOL.True;
                            shape.Protection.LockWidth.Value = BOOL.True;
                            shape.Protection.LockHeight.Value = BOOL.True;
                            shape.Protection.LockRotate.Value = BOOL.True;
                            shape.Protection.LockVtxEdit.Value = BOOL.True;
                            shape.Protection.LockAspect.Value = BOOL.True;
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