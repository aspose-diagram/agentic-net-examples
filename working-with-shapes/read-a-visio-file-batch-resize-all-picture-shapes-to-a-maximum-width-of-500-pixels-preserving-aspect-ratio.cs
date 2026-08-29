using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output_resized.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Maximum width in pixels and conversion to inches (assuming 96 DPI)
            const double maxWidthPixels = 500;
            const double dpi = 96.0;
            double maxWidthInches = maxWidthPixels / dpi;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Process only picture (foreign) shapes
                    if (shape.Type == TypeValue.Foreign)
                    {
                        double currentWidth = shape.XForm.Width.Value;
                        double currentHeight = shape.XForm.Height.Value;

                        // Resize if width exceeds the maximum
                        if (currentWidth > maxWidthInches)
                        {
                            double scale = maxWidthInches / currentWidth;
                            double newWidth = maxWidthInches;
                            double newHeight = currentHeight * scale;

                            shape.XForm.Width.Value = newWidth;
                            shape.XForm.Height.Value = newHeight;
                        }
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
