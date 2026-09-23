using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Work with the first page (index 0)
                Page page = diagram.Pages[0];

                // Initialize bounding box extremes
                double minX = double.MaxValue;
                double maxX = double.MinValue;

                // Iterate over all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Calculate left and right extents of the shape
                    double left = shape.XForm.PinX.Value - shape.XForm.Width.Value / 2.0;
                    double right = shape.XForm.PinX.Value + shape.XForm.Width.Value / 2.0;

                    if (left < minX) minX = left;
                    if (right > maxX) maxX = right;
                }

                // If no shapes were found, report and skip resizing
                if (minX == double.MaxValue)
                {
                    Console.WriteLine("No visible shapes found on the page.");
                }
                else
                {
                    // Compute the required page width to tightly fit all shapes
                    double newWidth = maxX - minX;

                    // Apply the new width to the page
                    page.PageSheet.PageProps.PageWidth.Value = newWidth;

                    Console.WriteLine($"Adjusted page width to {newWidth} inches to fit all shapes.");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
