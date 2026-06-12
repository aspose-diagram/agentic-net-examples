using System;
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
                // Path for the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the area threshold (in square inches)
                double areaThreshold = 1.0;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve width and height from the shape's XForm
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Calculate the shape's area
                        double area = width * height;

                        // If the area exceeds the threshold, enable fill inheritance
                        if (area > areaThreshold)
                        {
                            // Apply inherited fill foreground color
                            shape.Fill.FillForegnd.Value = shape.InheritFill.FillForegnd.Value;
                            // Apply inherited fill background color
                            shape.Fill.FillBkgnd.Value = shape.InheritFill.FillBkgnd.Value;
                            // Apply inherited fill pattern
                            shape.Fill.FillPattern.Value = shape.InheritFill.FillPattern.Value;
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