using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the area threshold (in square inches)
            double areaThreshold = 1.0;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Calculate shape area using width and height from XForm
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;
                    double area = width * height;

                    // If the area exceeds the threshold, apply inherited fill values
                    if (area > areaThreshold)
                    {
                        // Copy inherited fill foreground color
                        shape.Fill.FillForegnd.Value = shape.InheritFill.FillForegnd.Value;

                        // Copy inherited fill background color
                        shape.Fill.FillBkgnd.Value = shape.InheritFill.FillBkgnd.Value;

                        // Copy inherited fill pattern
                        shape.Fill.FillPattern.Value = shape.InheritFill.FillPattern.Value;
                    }
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
